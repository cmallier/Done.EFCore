using StaticApp.Entities;

namespace StaticApp;
internal static class FakeData
{
    // create a static property that return a Livre
    public static Livre Complete
    {
        get
        {
            return new Livre()
            {
                Titre = "Livre1"
            };
        }
    }
}

