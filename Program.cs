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

int db = 0;
float sum = 0F;
foreach (var allat in allatok)
{
    if (allat.Faj == "Zebra")
    {
        db += 1;
        sum += allat.Eletkor;
    }
}
Console.WriteLine($"zebrak atlag eletkora (progtetel): {sum / db}");

//MEGSZÁMLÁLÁS

//zsiráfok száma?
var zsirafokSzama = allatok.Count(a => a.Faj == "Zsiráf");

//mekkora az állatkert?
var osszMeret = allatok
    .DistinctBy(a => a.Reszleg.Nev)
    .Sum(a => a.Reszleg.Meret);

//állatok összéletkora?
var allatokOsszEletkora = allatok.Sum(a => a.Eletkor);

//legidősebb állat kora: (VALUE)
var legidosebbAllatEletkora = allatok.Max(a => a.Eletkor);
Console.WriteLine($"legnagyobb eletkor: {legidosebbAllatEletkora}");

//legidősebb állat: (OBJECT)
var legidosebbAllat = allatok.MaxBy(a => a.Eletkor);
Console.WriteLine($"legidősebb állat: {legidosebbAllat}");

//van-e láma az állatkertben?
var vanELama = allatok.Any(a => a.Faj == "Láma");
Console.WriteLine(vanELama ? "van láma" : "nincs láma");

//van-e 5 évesnél idősebb csimpánz?
var vanEOregCsimpanz = allatok.Any(a => a.Faj == "Csimpánz" && a.Eletkor > 5);
Console.WriteLine($"{(vanEOregCsimpanz ? "van" : "nincs")} 5 évesnél idősebb csimpánz");

//van-e minden állatnak neve
var vanMindnekNeve = allatok.All(a => a.Nev is not null);
Console.WriteLine($"{(vanMindnekNeve ? "van" : "nincs")} minden állatnak neve");

//van-e minden állatnak gondozója
var vanMindnekGondozoja = allatok.All(a => a.Gondozo is not null);
Console.WriteLine($"{(vanMindnekGondozoja ? "van" : "nincs")} minden állatnak gondozója");

//.First()
// visszaadja a pred.-nek megfelelő rendre első objektumot a kollekcióból (ha van ilyen)
// ha nincs -> exceptionra fut

//var altalamGondozottAllat = allatok.First(a => a.Gondozo.Nev == "Juhász Zoltán");

//var elsoSzavannaiAllat = allatok.First(a => a.Reszleg.Nev.ToLower().Contains("szavanna"));
//Console.WriteLine(elsoSzavannaiAllat);

var elsoSzavannaiAllat = allatok.FirstOrDefault(a => a.Reszleg.Nev.ToLower().Contains("szavanna"));
Console.WriteLine(elsoSzavannaiAllat);


//.FirstOrDefault() (Find())
// visszaadja a pred.-nek megfelelő rendre első objektumot a kollekcióból (ha van ilyen)
// ha nincs -> akkor 'default' értéked ad vissza, ami ref. típusok esetében null, value típusok esetében "zéró".
//var altalamGondozottAllat = allatok.FirstOrDefault(a => a.Gondozo.Nev == "Juhász Zoltán");
//Console.WriteLine(altalamGondozottAllat is null);



//C#-ban:
//minden class -> referencia típus,
//minden struct -> érték típus

//.Last() >> rendre utolsó (azaz lista esetében legnagyobb indexű)
//.LastOrDefault()

//.Single()
//ha van találat a pred.-re ÉS CSAK egyetlen egy találat van -> akkor visszadja a találatnak megfelelő objektumot
//ha TÖBB a pred.-nek megfelelő találat IS lenne:
//  -> exception (Sequence contains more than one matching element)
//ha EGYÁLTALÁN NINCS találat
//  -> exception (Sequence contains no matching element)

var simba = allatok.Single(a => a.Nev is not null && a.Nev.ToLower() == "nyakigláb");
Console.WriteLine(simba);
var egyetlenZsiraf = allatok.SingleOrDefault(a => a.Faj == "Láma");
Console.WriteLine(egyetlenZsiraf is null);

//.SingleOrDefault()
//ha van találat a pred.-re ÉS CSAK egyetlen egy találat van -> akkor visszadja a találatnak megfelelő objektumot
//ha TÖBB a pred.-nek megfelelő találat IS lenne:
//  -> exception (Sequence contains more than one matching element)
//ha EGYÁLTALÁN NINCS találat
//  -> default  (null vagy 'zéró')



//szintén "kvázi" 'eldontés tétele' DE NEM LINQ (hanem collections.generic)
//int[] t = [1, 2, 4, 5, 6];
//Console.WriteLine(t.Contains(2));

//SELECT AVG(eletkor) FROM allatok WHERE faj LIKE 'zebra'


var oroszlanok = allatok.Where(a => a.Faj == "Oroszlán");

Console.WriteLine("oroszlánok:");
foreach (var allat in oroszlanok) Console.WriteLine($"- {allat}");

//Dictionary<string, List<Allat>> aszr = [];
//foreach (var allat in allatok)
//{
//    _ = aszr.TryAdd(allat.Reszleg.Nev, []);
//    aszr[allat.Reszleg.Nev].Add(allat);
//}
//Console.WriteLine("allatok szama reszlegenkent:");
//foreach (var kvp in aszr)
//{
//    Console.WriteLine($"\t{kvp.Key} lakói:");
//    foreach (var allat in kvp.Value)
//    {
//        Console.WriteLine($"\t\t-{allat}");
//    }
//}

var arsz = allatok.GroupBy(a => a.Reszleg.Nev);
foreach (var item in arsz) Console.WriteLine($"{item.Key}: {item.Count()}");

//int[] t = [3, 2, 1, 5, 0, 10];
//t.Sort();
//Console.WriteLine(string.Join(", ", t));

Console.WriteLine("---------------------------------------------");

var kszcs = allatok.OrderByDescending(a => a.Eletkor).ThenBy(a => a.Gondozo.Nev);
foreach (var allat in kszcs) Console.WriteLine(allat);


var avg = allatok.Where(x => x.Faj == "Zebra").Average(x => x.Eletkor);

//IEnumerable<int> a = [2, 3, 4];
//IEnumerable<int> b = [3, 4, 5];
//var c = a.Union(b);
//Console.WriteLine(string.Join(", ", c));

Console.WriteLine("--------------");

//var x = allatok.DistinctBy(a => a.Gondozo.Nev).Select(a => new { a.Gondozo.Nev, a.Gondozo.Szakterulet});
//foreach (var gondozo in x) Console.WriteLine($"- {gondozo.Nev} ({gondozo.Szakterulet})");

var gondozok = allatok.Select(a => a.Gondozo.Nev).Distinct();
foreach (var gn in gondozok) Console.WriteLine(gn);

//Console.WriteLine($"zebrak atlageletkora (linq): {avg}");

//LINQ -> language integrated query

//megszámlálás                                      -> count()
//sorozatszámítás (összegzés)                       -> sum()
//szélsőérték meghatározás (min, max)               -> min(), max()

//lineáris keresés, kiválasztás, eldöntés,          -> select top 1, limit 1

//---

//kiválogatás                                       -> where, having
//csoportosítás (szétválogatás)                     -> groub by

//rendezési algoritmusok (egyszerű cserés, buborék) -> order by, order by desc,

//unió      -> .Union()    v .UnionBy(x => x...),
//metszet   -> .Intesect() v .IntercescBy(x => x....)
//különbség -> .Except()   v .ExceptBy(x => x...)