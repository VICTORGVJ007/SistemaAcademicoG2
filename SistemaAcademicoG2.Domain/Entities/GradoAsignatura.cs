using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
   public class GradoAsignatura
    {
        public int IdGradoAsignatura { get; set; }
        public int IdGrado { get; set; }
        public int IdAsignatura { get; set; }

        public int Estado { get; set; }

    }
}
