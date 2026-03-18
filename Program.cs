using System.Text;

const string AllatokTxt = "..\\..\\..\\data\\allatok.txt";

List<Allat> allatok = [];

using StreamReader sr = new(AllatokTxt, Encoding.UTF8);
_ = sr.ReadLine();
while (!sr.EndOfStream)
{
    string[] v = sr.ReadLine()!.Split(';');
    allatok.Add(new()
    {
        Nev = v[1] == "NULL" ? null : v[1],
        Faj = v[2],
        Eletkor = float.Parse(v[3].Replace('.', ',')),
        Reszleg = new()
        {
            Nev = v[5],
            Meret = int.Parse(v[6]),
        },
        Gondozo = new()
        {
            Nev = v[8],
            Szakterulet = v[9],
        },
    });
}

Console.WriteLine($"allatok szama: {allatok.Count} db");