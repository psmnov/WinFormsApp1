using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    
    public interface IPerson
    {
        int? CardNumber {  get; set; }
        string Name {  get; set; }
        string HashPassword { get; set; }
        DateTime Birthday { get; set; }
        
        int calcAge(DateTime date);
    }
    
}
