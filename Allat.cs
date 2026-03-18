class Allat
{
    public string? Nev { get; set; }
    public string Faj { get; set; }
    public float Eletkor { get; set; }
    public Reszleg Reszleg { get; set; }
    public Gondozo Gondozo { get; set; }

    public override string ToString() =>
        $"\t{Faj} ({Eletkor:0.0} éves), neve: {(Nev is null ? "még nincs" : Nev)}";
}