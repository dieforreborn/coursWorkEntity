using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using courseWorkEntity.Model;
using courseWorkEntity.Windows;
using Microsoft.Office.Interop.Word;


namespace courseWorkEntity.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page
    {
        private int _userId;
        public HomePage(int userId)
        {                                
            InitializeComponent();
            _userId = userId;
            fillProfileTextBox();
            fillMenuReport();
            //fillGroupFourYearStudent();
            fillFunctions fill = new fillFunctions();
            fill.fillComboBoxGroup(groupComboBox);
            fill.fillComboBoxSpec(spechComboBox);
            searchSurnameTextBox();
        }

        public void fillProfileTextBox()
        {
            using (var db = new colledgeDepartmentEntities())
            {
                var userInfo = (from user in db.users
                                   where user.idUser == _userId
                                   select new
                                   {
                                       surnameUser = user.surnameUser,
                                       nameUser = user.nameUser,
                                       patronymicUser = user.patronymicUser
                                   }).FirstOrDefault();
                if (userInfo != null)
                {
                    userTextBox.Text = $"Вы вошли под записью: {userInfo.nameUser} {userInfo.surnameUser} {userInfo.patronymicUser}";
                }
                else
                {
                    userTextBox.Text = "Пользователь";
                }
                
            }
        }


        public void fillMenuReport()
        {
            using (var db = new colledgeDepartmentEntities())
            {
                var groups = db.groups.Select(g => g.idGroup.ToString()).ToList();
                var disciplines = db.disciplines.Select(d => d.nameDisciplines.ToString()).ToList();

                foreach (var discipline in disciplines)
                {
                    var menuGroupDist = new MenuItem
                    {
                        Header = discipline,
                        Tag = discipline
                    };

                    menuGroupDist.Click += disciplineClick;
                    menuGroupDistMenu.Items.Add(menuGroupDist);

                }

                foreach (var groupName in groups)
                {
                    var menuItemReport = new MenuItem
                    {
                        Header = groupName,
                        Tag = groupName
                    };

                    menuItemReport.Click += reportClick;
                    reportItemMenu.Items.Add(menuItemReport);

                    var menuSexGroup = new MenuItem 
                    {
                        Header = groupName,
                        Tag = groupName
                    };

                    menuSexGroup.Click += statSexClick; 
                    groupSexMenu.Items.Add(menuSexGroup);

                    var menuSemGroup = new MenuItem
                    {
                        Header = groupName,
                        Tag = groupName
                    };

                    menuSemGroup.Click += statSemClick;
                    groupSemMenu.Items.Add(menuSemGroup);

                    var menuGroupTrans = new MenuItem
                    {
                        Header = groupName,
                        Tag = groupName
                    };

                    menuGroupTrans.Click += groupTransClick;
                    groupTransMenu.Items.Add(menuGroupTrans);
                }
            }
        }

        private void disciplineClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                string discipline = menuItem.Tag as string;
                using (var db = new colledgeDepartmentEntities())
                {
                    var results = db.getDisciplineStatistics(discipline).ToList();
                    if (results.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                        object missing = System.Reflection.Missing.Value;
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Text = "Отчет по дисциплине: " + discipline;
                        para1.Range.InsertParagraphAfter();

                        int rowCount = results.Count + 1;
                        int colCount = 7;

                        Table dataTable = document.Tables.Add(para1.Range, rowCount, colCount, ref missing, ref missing);
                        dataTable.Borders.Enable = 1;

                        dataTable.Cell(1, 1).Range.Text = "№ семестра";
                        dataTable.Cell(1, 2).Range.Text = "Учебный год";
                        dataTable.Cell(1, 3).Range.Text = "Дисциплина";
                        dataTable.Cell(1, 4).Range.Text = "Кол-во студентов на 5";
                        dataTable.Cell(1, 5).Range.Text = "Кол-во студентов на 4-5";
                        dataTable.Cell(1, 6).Range.Text = "Кол-во студентов на 3-4-5";
                        dataTable.Cell(1, 7).Range.Text = "Кол-во студентов на 2";

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var result = results[row - 2];
                            dataTable.Cell(row, 1).Range.Text = result.semesterNumber.ToString();
                            dataTable.Cell(row, 2).Range.Text = result.academicYear.ToString();
                            dataTable.Cell(row, 3).Range.Text = result.disciplineName;
                            dataTable.Cell(row, 4).Range.Text = result.countFive.ToString();
                            dataTable.Cell(row, 5).Range.Text = result.countFourFive.ToString();
                            dataTable.Cell(row, 6).Range.Text = result.countThreeFourFive.ToString();
                            dataTable.Cell(row, 7).Range.Text = result.countTwo.ToString();
                        }

                        winword.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Нет студентов в группе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        private void groupTransClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                string groupName = menuItem.Tag as string;
                int hyphenIndex = groupName.IndexOf('-');

                if (hyphenIndex >= 0)
                {
                    string prefix = groupName.Substring(0, hyphenIndex);
                    string suffix = groupName.Substring(hyphenIndex + 1);
                    if (int.TryParse(suffix, out int suffixNumber))
                    {
                        suffixNumber += 100;
                        string newGroupName = $"{prefix}-{suffixNumber}";
                        using (var db = new colledgeDepartmentEntities())
                        {
                            if (db.groups.Any(g => g.idGroup == newGroupName))
                            {
                                int request = db.transGroup(groupName, newGroupName);
                                if (request > 0)
                                {
                                    db.transGroup(groupName, newGroupName);
                                    MessageBox.Show($"Студенты {groupName} переведены в {newGroupName}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                                    searchSurnameTextBox();
                                } else
                                {
                                    MessageBox.Show("Ошибка перевода", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                } 
                                
                            }
                            else
                            {
                                MessageBox.Show("Нет группы на курс старше, для ее создания обратитесь к администратору", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }

     

        private void statSexClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                string groupName = menuItem.Tag as string;
                using (var db = new colledgeDepartmentEntities())
                {
                    var rezults = db.countSexGroup(groupName).ToList();
                    if (rezults.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                        object missing = System.Reflection.Missing.Value;
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Text = "Половая статистика по группе: " + groupName;
                        para1.Range.InsertParagraphAfter();

                        int rowCount = rezults.Count + 1;
                        int colCount = 4;

                        Table dataTable = document.Tables.Add(para1.Range, rowCount, colCount, ref missing, ref missing);

                        dataTable.Borders.Enable = 1;

                        dataTable.Cell(1, 1).Range.Text = "Группа ";
                        dataTable.Cell(1, 2).Range.Text = "Мужчины ";
                        dataTable.Cell(1, 3).Range.Text = "Женщины ";
                        dataTable.Cell(1, 4).Range.Text = "Всего студентов ";

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var student = rezults[row - 2];
                            dataTable.Cell(row, 1).Range.Text = student.idGroup;
                            dataTable.Cell(row, 2).Range.Text = student.sexM.ToString();
                            dataTable.Cell(row, 3).Range.Text = student.sexF.ToString();
                            dataTable.Cell(row, 4).Range.Text = student.totalStudents.ToString();
                        }

                        winword.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Нет студентов в группе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void statSemClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                string groupName = menuItem.Tag as string;
                using (var db = new colledgeDepartmentEntities())
                {
                    var results = db.getEvalutiionGroup(groupName).ToList();
                    if (results.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                        object missing = System.Reflection.Missing.Value;
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Text = "Успеваемость по группе: " + groupName;
                        para1.Range.InsertParagraphAfter();

                        int rowCount = results.Count + 1;
                        int colCount = 7;

                        Table dataTable = document.Tables.Add(para1.Range, rowCount, colCount, ref missing, ref missing);
                        dataTable.Borders.Enable = 1;

                        dataTable.Cell(1, 1).Range.Text = "№ семестра";
                        dataTable.Cell(1, 2).Range.Text = "Учебный год";
                        dataTable.Cell(1, 3).Range.Text = "Группа";
                        dataTable.Cell(1, 4).Range.Text = "Кол-во студентов на 5";
                        dataTable.Cell(1, 5).Range.Text = "Кол-во студентов на 4-5";
                        dataTable.Cell(1, 6).Range.Text = "Кол-во студентов на 3-4-5";
                        dataTable.Cell(1, 7).Range.Text = "Кол-во студентов на 2";

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var result = results[row - 2];
                            dataTable.Cell(row, 1).Range.Text = result.numberSemester.ToString();
                            dataTable.Cell(row, 2).Range.Text = result.academicYear.ToString();
                            dataTable.Cell(row, 3).Range.Text = result.idGroup;
                            dataTable.Cell(row, 4).Range.Text = result.countFive.ToString();
                            dataTable.Cell(row, 5).Range.Text = result.countFourFive.ToString();
                            dataTable.Cell(row, 6).Range.Text = result.countThreeFourFive.ToString();
                            dataTable.Cell(row, 7).Range.Text = result.countTwo.ToString();
                        }

                        winword.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Нет студентов в группе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
                }
            }
        }

        private void colledgeSexMenu_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                using (var db = new colledgeDepartmentEntities())
                {
                    var rezults = db.collegdeSexView.ToList();
                    if (rezults.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                        object missing = System.Reflection.Missing.Value;
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Text = "Отчет по группам: ";
                        para1.Range.InsertParagraphAfter();

                        int rowCount = rezults.Count + 1;
                        int colCount = 4;

                        Table dataTable = document.Tables.Add(para1.Range, rowCount, colCount, ref missing, ref missing);

                        dataTable.Borders.Enable = 1;

                        dataTable.Cell(1, 1).Range.Text = "Группа ";
                        dataTable.Cell(1, 2).Range.Text = "Мужчины ";
                        dataTable.Cell(1, 3).Range.Text = "Женщины ";
                        dataTable.Cell(1, 4).Range.Text = "Всего студентов ";

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var result = rezults[row - 2];
                            dataTable.Cell(row, 1).Range.Text = result.idGroup;
                            dataTable.Cell(row, 2).Range.Text = result.sexM.ToString();
                            dataTable.Cell(row, 3).Range.Text = result.sexF.ToString();
                            dataTable.Cell(row, 4).Range.Text = result.totalStudents.ToString();
                        }

                        winword.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("В колледже нет студентов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void colledgeEvalutionMenu_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                using (var db = new colledgeDepartmentEntities())
                {
                    var rezults = db.getEvalutiionColledge().ToList();
                    if (rezults.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                        object missing = System.Reflection.Missing.Value;
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Text = "Отчет по группам: ";
                        para1.Range.InsertParagraphAfter();

                        int rowCount = rezults.Count + 1;
                        int colCount = 6;

                        Table dataTable = document.Tables.Add(para1.Range, rowCount, colCount, ref missing, ref missing);

                        dataTable.Borders.Enable = 1;

                        dataTable.Cell(1, 1).Range.Text = "Номер семестра:";
                        dataTable.Cell(1, 2).Range.Text = "Учебный год:";
                        dataTable.Cell(1, 3).Range.Text = "Кол-во учащихся на 5:";
                        dataTable.Cell(1, 4).Range.Text = "Кол-во учащихся на 4-5:";
                        dataTable.Cell(1, 5).Range.Text = "Кол-во учащихся на 3-4-5:";
                        dataTable.Cell(1, 6).Range.Text = "Кол-во учащихся на 2:";

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var result = rezults[row - 2];
                            dataTable.Cell(row, 1).Range.Text = result.numberSemester.ToString();
                            dataTable.Cell(row, 2).Range.Text = result.academicYear.ToString();
                            dataTable.Cell(row, 3).Range.Text = result.countFive.ToString();
                            dataTable.Cell(row, 4).Range.Text = result.countFourFive.ToString();
                            dataTable.Cell(row, 5).Range.Text = result.countThreeFourFive.ToString();
                            dataTable.Cell(row, 6).Range.Text = result.countTwo.ToString();
                        }

                        winword.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("В колледже нет студентов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void reportClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                string groupName = menuItem.Tag as string;
                using (var db = new colledgeDepartmentEntities())
                {
                    var rezults = db.getReportAllGroup(groupName).ToList();
                    if (rezults.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application(); 
                        object missing = System.Reflection.Missing.Value;                                                    
                        Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Text = "Отчет по группе: " + groupName;
                        para1.Range.InsertParagraphAfter();

                        int rowCount = rezults.Count + 1;
                        int colCount = 3;

                        Table dataTable = document.Tables.Add(para1.Range, rowCount, colCount, ref missing, ref missing);

                        dataTable.Borders.Enable = 1;

                        dataTable.Cell(1, 1).Range.Text = "Фамилия "; 
                        dataTable.Cell(1, 2).Range.Text = "Имя " ;
                        dataTable.Cell(1, 3).Range.Text = "Отчество ";

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var student = rezults[row-2];
                            dataTable.Cell(row, 1).Range.Text = student.surname;
                            dataTable.Cell(row, 2).Range.Text = student.name;
                            dataTable.Cell(row, 3).Range.Text = student.patronymic;
                        }

                        winword.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Нет студентов в группе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        public void searchSurnameTextBox()
        {
            using (var db = new colledgeDepartmentEntities())
            {
                var request = db.students.Join(db.history, a => a.idStudent, b => b.idStudent, (a, b) => new { a, b }).Where(o => o.b.dateLayoff == null);

                if (surnameTextBox.Text != "")
                {
                    string searchText = surnameTextBox.Text.Trim();
                    request = request.Where(o => o.a.surname.Contains(searchText));
                }

                if (groupComboBox.SelectedIndex != groupComboBox.Items.Count - 1 && groupComboBox.SelectedIndex != -1)
                {
                    request = request.Where(o => o.b.idGroup == (groupComboBox.SelectedValue.ToString()));
                }

                if (spechComboBox.SelectedIndex != spechComboBox.Items.Count - 1 && spechComboBox.SelectedIndex != -1)
                {
                    var groups = db.groups.Where(a => a.idSpecialities == spechComboBox.SelectedValue.ToString()).Select(a => a.idGroup).ToList();
                    request = request.Where(o => groups.Any(p => p == o.b.idGroup));
                }

                var result = request.Select(a => new { a.a.idStudent, a.a.surname, a.a.name, a.a.patronymic, a.b.idGroup, a.a.sex, a.a.dateOfBirth }).ToList();
                homeDataGrid.ItemsSource = result;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void surnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchSurnameTextBox();
        }

        private void groupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchSurnameTextBox();
        }

        private void spechComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchSurnameTextBox();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addStudent window = new addStudent(false);
            if (window.ShowDialog() == true) 
            {
                searchSurnameTextBox();
            }
            else
            {
                MessageBox.Show("Добавление отменено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void homeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (homeDataGrid.SelectedItem != null)
            {
                editButton.IsEnabled = true;
                deleteButton.IsEnabled = true;
            } else
            {
                editButton.IsEnabled = false;
                deleteButton.IsEnabled = false;
            }
            
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = GetStudentInfo();
            addStudent window = new addStudent(true, selectedStudent);
            if (window.ShowDialog() == true)
            {
                searchSurnameTextBox();
            }
            else
            {
                MessageBox.Show("Редактирование отменено.","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new colledgeDepartmentEntities())
            {
                if (homeDataGrid.SelectedItem != null)
                {
                    int selectedId = (int)homeDataGrid.SelectedValue;
                    try
                    {
                        db.deleteStudent(selectedId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    searchSurnameTextBox();
                }
            }
        }

        public studentInfo GetStudentInfo()
        {
            var selectedData = (dynamic)homeDataGrid.SelectedItem;
            var selectedStudent = new studentInfo
            {
                Id = selectedData.idStudent,
                Surname = selectedData.surname,
                Name = selectedData.name,
                Patronymic = selectedData.patronymic,
                Sex = selectedData.sex,
                DateOfBirth = selectedData.dateOfBirth.ToString("dd.MM.yyyy"),
                Group = selectedData.idGroup
            };
            return selectedStudent;

        }
    }
}
