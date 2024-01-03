using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionTicketGenerator.AppCode
{
    public class ProgramHelper
    {
        public void BindCompo(ListControl control, ICollection dataSource, bool hasHeader = false, string displayMember = "Name", string valueMember = "ID")
        {
            switch (control)
            {
                case ComboBox cb:
                    cb.DataSource = null;
                    //cb.Items.Insert(0,new ComboboxItem("اختر قيمة...", "-1"));
                    break;
                case CheckedListBox clb:
                    clb.DataSource = null;
                    break;
            }
            if (hasHeader)
                if (CheckDatasource(dataSource))
                    ((IList)dataSource).Insert(0, new Item { ID = -1, Name = "اختر ..." });
                else
                    ((IList)dataSource).Insert(0, new StringItem { ID = "-1", Name = "اختر ..." });
            control.DataSource = dataSource;
            control.DisplayMember = displayMember;
            control.ValueMember = valueMember;
            if (null != dataSource && ((IList)dataSource).Count > 0)
                control.SelectedIndex = 0;
        }
        private bool CheckDatasource(ICollection dataSource)
        {

            var type = dataSource.GetType();

            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(IList<>))
                {
                    Type itemType = type.GetGenericArguments()[0];
                    // do something...
                    //var obj = Activator.CreateInstance(itemType);

                    foreach (var pi in itemType.GetProperties())
                    {
                        if (pi.Name == "ID")
                            if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(short))
                            {
                                return true;
                            }
                        break;
                    }

                    //string s = "x";
                    //((IList)dataSource).Insert(0, new { s = -1, name = "test" });
                    break;
                }
            }
            return false;
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        
        public ComboboxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
    class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    class StringItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
