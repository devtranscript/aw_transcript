//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace aw_transcript
{
    using System;
    using System.Collections.Generic;
    
    public partial class inf_juzgados
    {
        public System.Guid id_juzgado { get; set; }
        public int codigo_juzgado { get; set; }
        public Nullable<int> id_estatus { get; set; }
        public int id_especializa { get; set; }
        public string localidad { get; set; }
        public string numero { get; set; }
        public string callenum { get; set; }
        public Nullable<int> id_codigo { get; set; }
        public Nullable<System.Guid> id_tribunal { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
    }
}