
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitWpfTemplate1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>

    public partial class MainWindow : Window
    {
        
        public UIDocument uidoc;
        public Document doc;
       
        public MainWindow( UIDocument uIDocument, Document document)
        {
            InitializeComponent();
            uidoc = uIDocument;
            doc = document;
        }

        private void surebtn_Click(object sender, RoutedEventArgs e)
        {

            // 获取界面上输入的承台参数
            double length = double.Parse(textBox1.Text);
            double width = double.Parse(textBox2.Text);
            double height = double.Parse(textBox3.Text);

            // 创建承台实例
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            ICollection<Element> elementid1 = collector1.OfCategory(BuiltInCategory.OST_StructuralFoundation).OfClass(typeof(FamilySymbol)).ToElements();
            foreach (FamilySymbol item in elementid1)
            {
                if (item != null)
                {
                    if (item.Name== "承台001")
                    {
                      
                        using (Transaction trans = new Transaction(doc))
                        {
                            trans.Start("Create Rectangular Foot");
                            if (!item.IsActive)
                            {
                                item.Activate();
                                doc.Regenerate();
                            }
                            // 在原点创建承台实例
                            XYZ origin = new XYZ(0, 0, 0);
                            FamilyInstance rectFoot = doc.Create.NewFamilyInstance(origin, item, StructuralType.NonStructural);

                            // 设置承台实例的参数
                            rectFoot.LookupParameter("长度1").Set(length);
                            rectFoot.LookupParameter("宽度1").Set(width);
                            rectFoot.LookupParameter("承台高度").Set(height);

                            trans.Commit();
                        }
               
                    }
                }
                else
                {
                    TaskDialog.Show("Error", "无法找到“承台001”族类型");
                }
            }
          

        }
    }
}