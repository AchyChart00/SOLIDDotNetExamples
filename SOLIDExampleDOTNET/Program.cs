using System.Xml.Linq;

namespace SOLIDExampleDOTNET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sergio = new Person("Sergio");
            var alex = new Person("Alejandro");

            sergio.Speak();
            alex.Speak();

            var database = new DataBase();

            database.SaveToDatabase(sergio);
            database.SaveToDatabase(alex);

            var calculator = new SalaryCalculator();
            Console.WriteLine($"The {alex.Name} salary is {calculator.CalculateSalary(alex)}");
            var gabriel = new Manager("gabriel");
            Console.WriteLine($"The {gabriel.Name} salary is {calculator.CalculateSalary(gabriel)}");

            var director = new Manager("Jefe");
            Console.WriteLine($"The {director.Name} salary is {calculator.CalculateSalary(director)}");
        }
    }


    //principio de sustitución de liskov
    //establece que cada subclase debe de ser sustituida por la clase base y
    //el programa debe de seguir comportandose de la misma manera
    public class Person
    {
        public virtual decimal DailyRate => 0;
        public string Name { get; set; }
        public int Id { get; set; }

        public Person(string name)
        {
            this.Name = name;
        }

        public void Speak()
        {
            Console.WriteLine($"My name is {Name}");
        }

        //public void SaveToDatabase(Person person)
        //{
        //    Console.WriteLine($"Save to Database {person.Name}");
        //}

    }

    //Principio de responsabilidad única
    //Una clase solo debe tener una responsabilidad
    public class DataBase
    {
        public void SaveToDatabase(Person person)
        {
            Console.WriteLine($"Save to Database {person.Name}");
        }
    }

    //Principio de Abierto/cerrado
    //Establece que un objeto debe ser abierto a la extensión pero cerrado a la modificación.
    public class Employee : Person
    {
        public override decimal DailyRate => 100;
        public Employee(string name) : base(name)
        {
        }
    }

    public class Manager : Person
    {
        public override decimal DailyRate => 200;
        public Manager(string name) : base(name)
        {
        }
    }

    public class Director : Person
    {
        public Director(string name) : base(name)
        {

        }
        public override decimal DailyRate => 300;
    }

    public class SalaryCalculator
    {
        public decimal CalculateSalary(Person person)
        {
            return person.DailyRate * 365;
            //if (person is Employee)
            //{
            //    return 100 * 365;
            //}
            //else if(person is Manager)
            //{
            //    return 200 * 365;
            //}
            //else
            //{
            //    return 0;
            //}
        }
    }

    //Principio de segregación de interfaces
    //Establece que no se debe obligar a un cliente a implementar propiedades y métodos de una 
    //interfaz que no utilizara
    public interface IRepository
    {
        bool Create(Person person);
        Person GetPerson(int id);
        IEnumerable<Person> GetAll();
        bool Update(Person person);
        bool Delete(int id);
    }
    interface IGetTableRepository
    {
        Person GetPerson(int id);
        Person GetAll();
    }
    interface IUpdateTableRepository
    {
        bool Update(Person person);
    }

    interface ICreatableRepository
    {
        bool Create(Person person);
    }

    interface IDeleteRepository 
    {
        bool Delete(int id);
    }

    public class readonlyRepository : IGetTableRepository
    {
        Person IGetTableRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Person IGetTableRepository.GetPerson(int id)
        {
            throw new NotImplementedException();
        }
    }

    //Si quisieramos implementar todo el crud

    public class CrudRepository : IGetTableRepository, IUpdateTableRepository, ICreatableRepository, IDeleteRepository
    {
        bool ICreatableRepository.Create(Person person)
        {
            throw new NotImplementedException();
        }

        bool IDeleteRepository.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Person IGetTableRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Person IGetTableRepository.GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        bool IUpdateTableRepository.Update(Person person)
        {
            throw new NotImplementedException();
        }
    }

    //Principio de inversión de dependencias 
    //Establce que las clases no deben de depende de otras clases,
    //si no depender de las interfaces que esas clases implementan
    //este efecto tiene la de invertir la dirección de dependencias

}
