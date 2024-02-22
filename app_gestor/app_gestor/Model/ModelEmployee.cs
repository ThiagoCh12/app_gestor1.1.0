using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace app_gestor.Model
{
    [Table("Employees")]
    public class ModelEmployee
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        [NotNull]
        public string Nome {  get; set; }
        [NotNull]
        public string Turno { get; set; }
        [NotNull]
        public string Setor {  get; set; }
        public Boolean Favorito { get; set; }
        public ModelEmployee()
        {
            this.Id = 0;
            this.Nome = "";
            this.Turno = "";
            this.Setor = "";
            this.Favorito = false;
        }

    }
    
}
