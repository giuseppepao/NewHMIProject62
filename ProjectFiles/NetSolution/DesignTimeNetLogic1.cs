#region Using directives
using System;
using System.Linq;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.EventLogger;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.Alarm;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.OPCUAServer;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Recipe;
using FTOptix.Core;
using FTOptix.OPCUAClient;
#endregion

public class DesignTimeNetLogic1 : BaseNetLogic
{
    [ExportMethod]
    public void Method1()
    {
        // Insert code to be executed by the method
    }



    [ExportMethod]
   public void MyExportedMethod()
   {
       var myVar = Project.Current.GetVariable("Model/AnalogVariable1");
       if (myVar.GetByType<FTOptix.Core.Range>() is FTOptix.Core.Range myRange)
       {
           Log.Info("Min: " + myRange.GetVariable("Low").Value.Value.ToString());
           Log.Info("Max: " + myRange.GetVariable("High").Value.Value.ToString());
       }
       var euInfo = myVar.Get<EUInformation>("EngineeringUnits");
       if (euInfo != null)
       {
           var displayName = InformationModel.LookupTranslation(euInfo.DisplayName).Text;
           var description = InformationModel.LookupTranslation(euInfo.Description).Text;
           var commonCode = DecodeUnitId(euInfo.UnitId);
           Log.Info($"EU UnitID: {euInfo.UnitId} (codice UN/CEFACT: '{commonCode}'), Unità di misura: {displayName}, Description: {description}");
       }
   }

   // Negli standard OPC UA il UnitId è il codice UN/CEFACT (es. "CEL", "MTR")
   // codificato come byte ASCII di un intero. Lo decodifichiamo nel codice leggibile.
   private static string DecodeUnitId(int unitId)
   {
       var bytes = BitConverter.GetBytes(unitId);
       if (BitConverter.IsLittleEndian)
           Array.Reverse(bytes);

       // Rimuove i byte nulli iniziali e converte i restanti in caratteri ASCII
       var chars = bytes
           .SkipWhile(b => b == 0)
           .Select(b => (char)b)
           .ToArray();

       return new string(chars);
   }

}
