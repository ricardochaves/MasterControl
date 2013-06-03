using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styx.WoWInternals;

namespace AuctionWrapper
{
    public static class Helpers
    {
        public static int InBagCount(uint id)
        {
            var lua = string.Format("return GetItemCount({0})", id);
            return Lua.GetReturnVal<int>(lua, 0);
        }
    }
}
