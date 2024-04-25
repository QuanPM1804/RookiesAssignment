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
                var maleMembers = IsMale(members);
                foreach (var member in maleMembers)
                {
                    Console.WriteLine("Name: " + member.LastName + ' ' + member.FirstName);
                }
                break;
            case 2:
                var OldestMember = Oldest(members);
                Console.WriteLine("The Oldest: " + OldestMember.LastName + ' ' + OldestMember.FirstName);
                break;
            case 3:
                var FullNameOfMember = GetFullNameOfMembers(members);
                foreach (var member in FullNameOfMember)
                {
                    Console.WriteLine("Full Name: " + member.FullName);
                }
                break;
            case 4:
                var listsByBirthYear = YearOfBirth(members);
                Console.WriteLine("Members born in 2000:");
                foreach (var member in listsByBirthYear.Item1)
                {
                    Console.WriteLine("Name: " + member.LastName + ' ' + member.FirstName);
                }
                Console.WriteLine("Members born after 2000:");
                foreach (var member in listsByBirthYear.Item2)
                {
                    Console.WriteLine("Name: " + member.LastName + ' ' + member.FirstName);
                }
                Console.WriteLine("Members born before 2000:");
                foreach (var member in listsByBirthYear.Item3)
                {
                    Console.WriteLine("Name: " + member.LastName + ' ' + member.FirstName);
                }
                break;
            case 5:
                var FirstBornInHaNoi = BornInHaNoi(members);
                Console.WriteLine(FirstBornInHaNoi.LastName + ' ' + FirstBornInHaNoi.FirstName);
                break;
        }
    }

    public static List<Member> IsMale(List<Member> members)
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

    public static Member Oldest(List<Member> members)
    {
        var OldestMember = members[0];
        foreach (var member in members)
        {
            if (member.DateOfBirth < OldestMember.DateOfBirth)
            {
                OldestMember = member;
            }
        }
        return OldestMember;
    }

    public static List<FullNameOfMember> GetFullNameOfMembers(List<Member> members)
    {
        List<FullNameOfMember> fullNames = new List<FullNameOfMember>();
        foreach (var member in members)
        {
            fullNames.Add(new FullNameOfMember { FullName = $"{member.LastName} {member.FirstName}" });
        }
        return fullNames;
    }

    public static Tuple<List<Member>, List<Member>, List<Member>> YearOfBirth(List<Member> members)
    {
        List<Member> BornIn2000 = new List<Member>();
        List<Member> BornAfter2000 = new List<Member>();
        List<Member> BornBefore2000 = new List<Member>();
        foreach (var member in members)
        {
            switch (member.DateOfBirth.Year)
            {
                case var year when year > 2000:
                    BornAfter2000.Add(member);
                    break;
                case var year when year == 2000:
                    BornIn2000.Add(member);
                    break;
                case var year when year < 2000:
                    BornBefore2000.Add(member);
                    break;
            }
        }
        return Tuple.Create(BornIn2000, BornAfter2000, BornBefore2000);
    }

    public static Member BornInHaNoi(List<Member> members)
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

public class FullNameOfMember
{
    public string FullName { get; set; }
}
