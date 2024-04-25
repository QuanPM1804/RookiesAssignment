using System;
using System.Collections.Generic;

public class Member
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Birthplace { get; set; }
    public int Age { get; set; }
    public bool IsGraduated { get; set; }
    public string FullName => LastName + ' ' + FirstName;
}

public class Program
{
    public static void Main()
    {
        List<Member> members = new List<Member>
        {
            new Member { FirstName = "Van A", LastName = "Nguyen", Gender = "Male", DateOfBirth = new DateTime(2000, 1, 1), PhoneNumber = "0123456789", Birthplace = "Ha Noi", Age = 24, IsGraduated = true },
            new Member { FirstName = "Van B", LastName = "Nguyen", Gender = "Male", DateOfBirth = new DateTime(2001, 1, 1), PhoneNumber = "0123456789", Birthplace = "Ha Noi", Age = 23, IsGraduated = true },
            new Member { FirstName = "Van C", LastName = "Nguyen", Gender = "Female", DateOfBirth = new DateTime(2002, 1, 1), PhoneNumber = "0123456789", Birthplace = "Ha Noi", Age = 22, IsGraduated = true },
            new Member { FirstName = "Van D", LastName = "Nguyen", Gender = "Male", DateOfBirth = new DateTime(1999, 1, 1), PhoneNumber = "0123456789", Birthplace = "Ha Noi", Age = 25, IsGraduated = true }
        };
        while (true)
        {
            try
            {
                Console.WriteLine("Option: ");
                Console.WriteLine("1. Return a list of members who are Male");
                Console.WriteLine("2. Return the oldest one based on 'Age'");
                Console.WriteLine("3. Return a new list that contains Full Name only ( Full Name = Last Name + First Name)");
                Console.WriteLine("4. Return 3 lists:");
                Console.WriteLine("     List of members who have a birth year of 2000");
                Console.WriteLine("     List of members who have a birth year greater than 2000");
                Console.WriteLine("     List of members who have a birth year less than 2000");
                Console.WriteLine("5. Return the first person who was born in Ha Noi.");
                Console.Write("Enter your choice (1-5): ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        var maleMembers = MemberOperations.GetMaleMembers(members);
                        foreach (var member in maleMembers)
                        {
                            Console.WriteLine("Name: " + member.FullName);
                        }
                        break;
                    case 2:
                        var oldestMember = MemberOperations.GetOldestMember(members);
                        Console.WriteLine("The Oldest member: " + oldestMember.FullName);
                        break;
                    case 3:
                        var fullNames = MemberOperations.GetFullNames(members);
                        foreach (var fullName in fullNames)
                        {
                            Console.WriteLine("Full Name: " + fullName);
                        }
                        break;                    
                    case 4:
                        var listsByBirthYear = MemberOperations.SplitByBirthYear(members);
                        Console.WriteLine("Members born in 2000:");
                        foreach (var member in listsByBirthYear.Item1)
                        {
                            Console.WriteLine("Name: " + member.FullName);
                        }
                        Console.WriteLine("Members born after 2000:");
                        foreach (var member in listsByBirthYear.Item2)
                        {
                            Console.WriteLine("Name: " + member.FullName);
                        }
                        Console.WriteLine("Members born before 2000:");
                        foreach (var member in listsByBirthYear.Item3)
                        {
                            Console.WriteLine("Name: " + member.FullName);
                        }
                        break;
                    case 5:
                        var firstBornInHaNoi = MemberOperations.GetFirstBornInHaNoi(members);
                        if (firstBornInHaNoi != null)
                        {
                            Console.WriteLine(firstBornInHaNoi.FullName);
                        }
                        else
                        {
                            Console.WriteLine("No member was born in Ha Noi.");
                        }
                        break;
                
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
        }
    }
}
public class MemberOperations
{
    public static List<Member> GetMaleMembers(List<Member> members)
    {
        List<Member> maleMembers = new List<Member>();
        foreach (var member in members)
        {
            if (member.Gender.ToLower() == "male")
            {
                maleMembers.Add(member);
            }
        }
        return maleMembers;
    }

    public static Member GetOldestMember(List<Member> members)
    {
        var oldestMember = members[0];
        foreach (var member in members)
        {
            if (member.DateOfBirth < oldestMember.DateOfBirth)
            {
                oldestMember = member;
            }
        }
        return oldestMember;
    }

    public static List<string> GetFullNames(List<Member> members)
    {
        List<string> fullNames = new List<string>();
        foreach (var member in members)
        {
            fullNames.Add(member.FullName);
        }
        return fullNames;
    }

    public static Tuple<List<Member>, List<Member>, List<Member>> SplitByBirthYear(List<Member> members)
    {
        List<Member> bornIn2000 = new List<Member>();
        List<Member> bornAfter2000 = new List<Member>();
        List<Member> bornBefore2000 = new List<Member>();
        foreach (var member in members)
        {
            switch (member.DateOfBirth.Year)
            {
                case var year when year > 2000:
                    bornAfter2000.Add(member);
                    break;
                case var year when year == 2000:
                    bornIn2000.Add(member);
                    break;
                case var year when year < 2000:
                    bornBefore2000.Add(member);
                    break;
            }
        }
        return Tuple.Create(bornIn2000, bornAfter2000, bornBefore2000);
    }

    public static Member GetFirstBornInHaNoi(List<Member> members)
    {
        foreach (var member in members)
        {
            if (member.Birthplace == "Ha Noi")
            {
                return member;
            }
        }
        return null;
    }
}