#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
using FTOptix.OPCUAServer;
using FTOptix.OPCUAClient;
#endregion

public class RuntimeNetLogic2 : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    public double Somma(double a, double b)
    {
        return a + b;
    }

    public struct Coordinate
    {
        public double X;
        public double Y;
    }

    public Coordinate CalcolaPercorso(string nomeVariabile)
    {
        try
        {
            // Legge la variabile dal folder "model"
            var modelFolder = Project.Current.GetObject("Model");
            if (modelFolder == null)
            {
                Log.Error("Folder 'Model' non trovato");
                return new Coordinate { X = 0, Y = 0 };
            }

            var variabile = modelFolder.GetVariable(nomeVariabile);
            if (variabile == null)
            {
                Log.Error($"Variabile '{nomeVariabile}' non trovata in Model");
                return new Coordinate { X = 0, Y = 0 };
            }

            // Ottiene il valore (0-100)
            double valore = (double)variabile.Value;
            double parametro = valore / 100.0; // Normalizza a 0-1

            // Calcola le coordinate nel range 100-500
            double x = 100 + parametro * 400; // X varia da 100 a 500
            double y = 100 + 400 * Math.Abs(Math.Sin(parametro * Math.PI)); // Y varia da 100 a 500 seguendo una curva

            return new Coordinate { X = x, Y = y };
        }
        catch (Exception ex)
        {
            Log.Error($"Errore nel calcolo del percorso: {ex.Message}");
            return new Coordinate { X = 0, Y = 0 };
        }
    }
    
}
