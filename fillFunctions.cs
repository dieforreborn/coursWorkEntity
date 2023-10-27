using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using courseWorkEntity.Model;

namespace courseWorkEntity
{
    public class fillFunctions
    {
        public void fillComboBoxGroup(ComboBox nameComboBox)
        {
            using (var db = new colledgeDepartmentEntities())
            {
                var listGroups = (from g in db.groups select g.idGroup).ToList();
                listGroups.Add("...");
                nameComboBox.ItemsSource = listGroups;
            }
        }

        public void fillComboBoxSpec(ComboBox nameComboBox)
        {
            using (var db = new colledgeDepartmentEntities())
            {
                var listSpecialites = (from s in db.specialities select s.idSpecialities).ToList();
                listSpecialites.Add("...");
                nameComboBox.ItemsSource = listSpecialites;
            }
        }
        
        public void fillComboBoxSex(ComboBox nameComboBox)
        {
            List<string> sex = new List<string>();
            sex.Add("М");
            sex.Add("Ж");
            nameComboBox.ItemsSource = sex;
        }

    }
}
