using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    interface IPerson
    {        
        string Name { get; set; }
        string Surname { get; set; }
        string Patronymic { get; set; }
        string Phone { get; set; }
    }
}
