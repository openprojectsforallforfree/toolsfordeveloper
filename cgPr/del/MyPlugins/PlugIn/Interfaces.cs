using System;
using System.Data;

namespace PlugIn
{
    /// <summary>
    /// Generic plugin interface
    /// </summary>
    public interface IPlugin
    {
        Int32 Add(Int32 a, Int32 b);
        string Name { get; set; }
        IPluginHost Host { get; set; }
        void Show();
        string myVariable
        { get; set; }
    }
    public interface Base
    {
        void GenCreate_Beforelines();
        void GenCreate_loop_Fistline();
        void GenCreate_loop();
        void GenCreate_loop_Lastline();
        void GenCreate_Afterlines();
        string gencreate(DataTable dt);


    }

    /// <summary>
    /// The host
    /// </summary>
    public interface IPluginHost
    {
        bool Register(IPlugin ipi);
    }
}
