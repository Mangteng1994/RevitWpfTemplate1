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
        public MainWindow(UIDocument uIDocument, Document document)
        {
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
            FamilySymbol rectFootSymbol = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(x => x.FamilyName == "结构基础");
            if (rectFootSymbol != null)
            {
                using (Transaction trans = new Transaction(doc))
                {
                    trans.Start("Create Rectangular Foot");

                    // 在原点创建承台实例
                    XYZ origin = new XYZ(0, 0, 0);
                    FamilyInstance rectFoot = doc.Create.NewFamilyInstance(origin, rectFootSymbol, StructuralType.NonStructural);

                    // 设置承台实例的参数
                    //rectFoot.LookupParameter("基础长度").Set(length);
                    //rectFoot.LookupParameter("基础宽度").Set(width);
                    rectFoot.LookupParameter("基础厚度").Set(height);

                    trans.Commit();
                }
            }
            else
            {
                TaskDialog.Show("Error", "无法找到“020201承台矩形”族类型");
            }

        }
    }
}
