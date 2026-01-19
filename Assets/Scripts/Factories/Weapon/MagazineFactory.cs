public static class MagazineFactory
{
    public static Magazine CreateMagazine(MagazineData magazineData)
    {
        return new Magazine(magazineData);
    }
}
