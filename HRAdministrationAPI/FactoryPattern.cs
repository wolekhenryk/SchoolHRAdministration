using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAdministrationAPI;

public static class FactoryPattern<TK, T>
    where T : class, TK, new() {
  public static TK GetInstance() {
    TK objK = new T();
    return objK;
  }
}
