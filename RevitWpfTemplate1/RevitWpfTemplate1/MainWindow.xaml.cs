
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            SizeToContent = SizeToContent.WidthAndHeight;
            uidoc = uIDocument;
            doc = document;
        }

        private void surebtn_Click(object sender, RoutedEventArgs e)
        {

            // 获取界面上输入的承台参数
            double length = double.Parse(textBox1.Text) / 304.8;
            double width = double.Parse(textBox2.Text) / 304.8;
            double height = double.Parse(textBox3.Text) / 304.8;

            // 创建承台实例
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            ICollection<Element> elementid1 = collector1.OfCategory(BuiltInCategory.OST_StructuralFoundation).OfClass(typeof(FamilySymbol)).ToElements();
            foreach (FamilySymbol foundation in elementid1)
            {
                if (foundation != null)
                {
                    if (foundation.Name== "承台001")
                    {
                      
                        using (Transaction trans = new Transaction(doc))
                        {
                            trans.Start("Create Rectangular Foot");
                            if (!foundation.IsActive)
                            {
                                foundation.Activate();
                                doc.Regenerate();
                            }
                            // 在原点创建承台实例
                            XYZ origin = new XYZ(0, 0, 0);
                            FamilyInstance rectFoot = doc.Create.NewFamilyInstance(origin, foundation, StructuralType.NonStructural);

                            // 设置承台实例的参数
                            rectFoot.LookupParameter("长度1").Set(length);
                            rectFoot.LookupParameter("宽度1").Set(width);
                            rectFoot.LookupParameter("承台高度").Set(height);

                            trans.Commit();
                        }
               
                    }
                
                }
               
            }
            
            //获取界面上输入的桩基参数
            double pileLength = double.Parse(textBox4.Text) / 304.8;
            double pileDiameter = double.Parse(textBox5.Text) / 304.8;
            string layoutParameter = textBox6.Text;
            string distanceParameter = textBox7.Text;

            //计算排布参数
            int numColunms = int.Parse(layoutParameter.Split('*')[0]) ;
            int numRows= int.Parse(layoutParameter.Split('*')[1]);

            //计算距离参数
            double a = double.Parse(distanceParameter.Split('*')[0]) / 304.8;
            double b = double.Parse(distanceParameter.Split('*')[1]) / 304.8;
            double c = double.Parse(distanceParameter.Split('*')[2]) / 304.8;
            double d = double.Parse(distanceParameter.Split('*')[3]) / 304.8;

            // 创建桩基实例
           
            ICollection<Element> elementid2 = collector1.OfCategory(BuiltInCategory.OST_StructuralFoundation).OfClass(typeof(FamilySymbol)).ToElements();
            foreach (FamilySymbol pileSymbol in elementid2)
            {
                if (pileSymbol!=null)
                {
                    if (pileSymbol.Name=="桩基001")
                    {
                        using (Transaction trans = new Transaction(doc))
                        {
                            trans.Start("Create Piles");
                            //计算桩基圆心位置
                           
                            //创建桩基实例
                            for (int i = 0; i < numRows; i++)
                            {
                                for (int j = 0; j < numColunms; j++)
                                {
                                    double x = -length / 2 + b + c * i;
                                    double y = width / 2 - a - d * j;
                                    XYZ origin = new XYZ(x,y, -height);
                                    FamilyInstance pile = doc.Create.NewFamilyInstance(origin,pileSymbol,StructuralType.NonStructural);
                                    //设置桩基参数
                                    pile.LookupParameter("桩基高度").Set(pileLength);
                                    pile.LookupParameter("桩基直径").Set(pileDiameter);
                                }
                            }

                            trans.Commit();
                        }
                    }
                }
            }



        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        // 创建一个相对路径的Uri对象


        private void csbtn_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个新的 Image 对象
            var image = new Image();
            // 使用 BitmapImage 类加载相对路径的图片
            var bitmapImage = new BitmapImage(new Uri(@"F:\Rum\CushionCap202302\RevitWpfTemplate1\RevitWpfTemplate1\RevitWpfTemplate1\PIC\参数说明.png"));
            // 将图片设置为 Image 对象的 Source 属性
            image.Source = bitmapImage;

            // 创建一个新的 Window 对象
            var window = new Window
            {
                // 将 Image 对象设置为 Window 的 Content 属性
                Content = image,
                // 根据内容自动调整窗口大小
                SizeToContent = SizeToContent.WidthAndHeight
            };
            // 显示窗口，并等待用户关闭窗口
            window.ShowDialog();

        }

        private void csbtn2_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个新的 Image 对象
            var image = new Image();
            // 使用 BitmapImage 类加载相对路径的图片
            var bitmapImage = new BitmapImage(new Uri(@"F:\Rum\CushionCap202302\RevitWpfTemplate1\RevitWpfTemplate1\RevitWpfTemplate1\PIC\距离参数说明.png"));
            // 将图片设置为 Image 对象的 Source 属性
            image.Source = bitmapImage;

            // 创建一个新的 Window 对象
            var window = new Window
            {
                // 将 Image 对象设置为 Window 的 Content 属性
                Content = image,
                // 根据内容自动调整窗口大小
                SizeToContent = SizeToContent.WidthAndHeight
            };
            // 显示窗口，并等待用户关闭窗口
            window.ShowDialog();
        }
    }
}