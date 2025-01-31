using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Fundamentals.Controllers
{
    internal interface IBaseController
    {
        void AddItem();
        void ViewItems();
        void DeleteItem();
    }
}
