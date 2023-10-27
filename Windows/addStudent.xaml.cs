using System;
using System.Linq;
using System.Windows;
using courseWorkEntity.Model;
using System.Globalization;
using courseWorkEntity.Pages;
using System.Collections.Generic;

namespace courseWorkEntity.Windows
{
    /// <summary>
    /// Логика взаимодействия для addStudent.xaml
    /// </summary>
    public partial class addStudent : Window
    {
        public bool editMode;
        private studentInfo selectedStudent;
        public addStudent(bool editMode, studentInfo selectedStudent = null)
        {
            this.editMode = editMode;
            this.selectedStudent = selectedStudent;
            InitializeComponent();
            fillFunctions fill = new fillFunctions();
            fill.fillComboBoxGroup(addStudentGroup);
            fill.fillComboBoxSex(addStudentSex);
            if (editMode)
            {
                Title = "Редактирование студента";
                addSaveButton.Content = "Изменить";
                addStudentSurname.Text = selectedStudent.Surname;
                addStudentName.Text = selectedStudent.Name;
                addStudentPatronymic.Text = selectedStudent.Patronymic;
                addStudentGroup.Text = selectedStudent.Group;
                addStudentSex.Text = selectedStudent.Sex;
                datePicker.Text = selectedStudent.DateOfBirth;
            }
        }

        private bool checkGroup(string idGroup)
        {
            using (var db = new colledgeDepartmentEntities())
            {
                return db.groups.Any(g => g.idGroup == idGroup);
            }
        }

        public class ValidationResult
        {
            public bool IsValid;
            public DateTime ParsedDate;
        }
        public ValidationResult checkAllData()
        {
            ValidationResult result = new ValidationResult();
            bool isValid = true;
            List<string> errors = new List<string>();

            if (!(addStudentSurname.Text.Length <= 32 && addStudentSurname.Text != ""))
            {
                errors.Add("Ввведена пустая или слишком длинная фамилия");
            }

            if (!(addStudentName.Text.Length <= 32 && addStudentName.Text != ""))
            {
                errors.Add("Ввведено пустое или слишком длинное имя");
            }

            if (!(addStudentPatronymic.Text.Length <= 32))
            {
                errors.Add("ввведено длинное отчество");
            }

            if (!checkGroup(addStudentGroup.Text))
            {
                errors.Add("Неправильно набрана группа или ее не существует.");
            }


            if (!DateTime.TryParseExact(datePicker.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out DateTime parsedDate))
            {
                errors.Add("Неправильно введена дата рождения");
            }

            if (errors.Count > 0)
            {
                string errorMessage = string.Join(Environment.NewLine, errors);
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                result.IsValid = isValid;
                result.ParsedDate = parsedDate;
            }
            return result;
        }

        private void addSaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new colledgeDepartmentEntities())
            {
                if (checkAllData().IsValid)
                {
                    string parsedData = checkAllData().ParsedDate.ToString();
                    if (editMode == false)
                    {
                        DialogResult = true;
                        try
                        {
                            db.addNewStudent(addStudentSurname.Text,
                                        addStudentName.Text,
                                        addStudentPatronymic.Text,
                                        addStudentGroup.Text,
                                        addStudentSex.Text,
                                        parsedData);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else if (editMode == true)
                    {
                        DialogResult = true;
                        try
                        {
                            db.editNewStudent(selectedStudent.Id,
                                          addStudentSurname.Text,
                                          addStudentName.Text,
                                          addStudentPatronymic.Text,
                                          addStudentGroup.Text,
                                          addStudentSex.Text,
                                          parsedData);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        

                    }
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
