
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using RevitWpfTemplate1;

namespace RevitWpfTemplate1
{
    [Transaction(TransactionMode.Manual)]
    public class CreateRectangularFoot : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            MainWindow mainWindow = new MainWindow(uidoc, doc);
            mainWindow.ShowDialog();
            return Result.Succeeded;
            
        }
    }


}