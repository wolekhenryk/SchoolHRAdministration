using HRAdministrationAPI;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolHRAdministration;

public enum EmployeeType { Teacher, HeadOfDepartment, DeputyHeadMaster, HeadMaster }

internal class Program {
  public static void Main() {
    var employees = new List<IEmployee>();

    SeedData(employees);

    Console.WriteLine($"Total salaries: {employees.Sum(e => e.Salary)}");
    Console.Read();
  }

  public static void SeedData(List<IEmployee> employees) {
    var teacher1 =
        EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 1, "Jesse", "Pinkman", 95000);
    if (teacher1 != null)
      employees.Add(teacher1);

    var teacher2 =
        EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 2, "Howard", "Hamlin", 45000);
    if (teacher2 != null)
      employees.Add(teacher2);

    var headOfDep = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadOfDepartment, 3, "Charles",
                                                        "McGill", 65000);
    if (headOfDep != null)
      employees.Add(headOfDep);

    var depHeadMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 4,
                                                            "Bill", "Oakley", 95000);
    if (depHeadMaster != null)
      employees.Add(depHeadMaster);

    var headMaster =
        EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadMaster, 5, "Saul", "Goodman", 1000000);
    if (headMaster != null)
      employees.Add(headMaster);
  }
}

public class Teacher : EmployeeBase {
  public override decimal Salary => base.Salary * 1.02m;
}

public class HeadOfDepartment : EmployeeBase {
  public override decimal Salary => base.Salary * 1.03m;
}

public class DeputyHeadMaster : EmployeeBase {
  public override decimal Salary => base.Salary * 1.04m;
}

public class HeadMaster : EmployeeBase {
  public override decimal Salary => base.Salary * 1.05m;
}

public static class EmployeeFactory {
  public static IEmployee? GetEmployeeInstance(EmployeeType employeeType,
                                               int id,
                                               string firstName,
                                               string lastName,
                                               decimal salary) {
    var employee = employeeType switch {
      EmployeeType.Teacher => FactoryPattern<IEmployee, Teacher>.GetInstance(),
      EmployeeType.HeadOfDepartment => FactoryPattern<IEmployee, HeadOfDepartment>.GetInstance(),
      EmployeeType.DeputyHeadMaster => FactoryPattern<IEmployee, DeputyHeadMaster>.GetInstance(),
      EmployeeType.HeadMaster => FactoryPattern<IEmployee, HeadMaster>.GetInstance(),
      _ => null
    };

    if (employee != null) {
      employee.Id = id;
      employee.FirstName = firstName;
      employee.LastName = lastName;
      employee.Salary = salary;
    } else {
      throw new NullReferenceException();
    }

    return employee;
  }
}
