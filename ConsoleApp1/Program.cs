using System.ComponentModel;

internal class Program
{
    struct Student 
    {
        public string nume;
        public int nota;
    }
    private static List<Student> CitesteStudenti(string filename)
    {
        var students = new List<Student>();
        foreach (string s in File.ReadAllLines(filename))
        {
            var parts = s.Split(";");
            Student student= new Student();
            student.nume = parts[0];
            student.nota = int.Parse(parts[1]);    

            students.Add(student);  
        }

        return students;
    }

    private static void AfisareStudenti(List<Student> studenti)
    {
        foreach(var student in studenti) {
            Console.WriteLine($"{student.nume}: {student.nota}");
        }
    }

    private static void AfisareRestantieri(List<Student> studenti)
    {
        Console.WriteLine("RESTANT");
        foreach (var student in studenti)
        {
            if (student.nota <5)
                Console.WriteLine($"{student.nume}: {student.nota}");
        }
    }
    private static int Compara(Student a, Student b)
    {
        return a.nume.CompareTo(b.nume);
    }

    private static Student? Cauta(List<Student> studenti, string nume)
    {
        foreach(Student student in studenti) { 
            if(student.nume.CompareTo(nume) == 0)
            {
                return student;
            }
        }
        return null;
    }   
    private static int ComparaNote(Student a, Student b)
    {
        //if (a.nota == b.nota)
        //    return a.nume.CompareTo(b.nume);

        //if (a.nota > b.nota) return -1;
        //return 1;

        int rez = a.nota.CompareTo(b.nota); //crescatoare cu +, descrescatoare -
        if(rez==0) return a.nume.CompareTo(b.nume);
        return rez;
    }

    private static void Main(string[] args)
    {
        List<Student> studenti = null;
        do
        {
            Console.WriteLine (
@"Apasati :
c — citire fisier
a — afisare lista studenti
r — afisare lista restantieri
x — iesire
s - sortati alfabetic studenti
n - sortati nota descrescator studenti
f - cautati student");


            string[] input = Console.ReadLine().Split(" ");
            char optiune = char.Parse(input[0]);
            switch (optiune) {
                case 'c':
                    string fisier = input.Length > 1 ? input[1] : Console.ReadLine();
                    studenti = CitesteStudenti(fisier);
                break;
                case 'a':
                    AfisareStudenti(studenti);
                break;
                case 'r':
                    AfisareRestantieri(studenti);
                break;
                case 's':
                    studenti.Sort(Compara);
                    AfisareStudenti(studenti);
                break;
                case 'n':
                    studenti.Sort(ComparaNote);
                    AfisareStudenti(studenti);
                break;
                case 'f':
                    string cautat = input.Length > 1 ? input[1] : Console.ReadLine();
                    Student? r = Cauta(studenti, cautat);
                    if (r == null)
                        Console.WriteLine("Nu a fost gasit");
                    else
                        Console.WriteLine("Student gasit");
                break;
            }

            if (optiune == 'x')
                break;

            Console.WriteLine();
            Console.WriteLine();
        } while (true);

    }
}