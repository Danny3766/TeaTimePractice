using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaTime.Models.ViewModels
{
    /// <summary>
    /// 購物車 ViewModel
    /// </summary>
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartModel> ShoppingCartList { get; set; }

        public double OrderTotal { get; set; }
    }
}
