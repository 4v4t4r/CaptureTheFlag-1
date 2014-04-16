using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag.Services
{
    public interface IMapModelService
    {
        void Create(string token);
        void Read(string token);
        void Update(string token);
        void Delete(string token);
    }
}
