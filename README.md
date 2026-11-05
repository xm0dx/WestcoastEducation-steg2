# Westcoast Education, steg 2

Inlämningsuppgift 2 i kursen Programmering med C# (ITHS).

En Avalonia-app för skrivbordet där utbildningsledare och administratörer kan
hantera kurser, studerande och lärare. Datan ligger i restdb.io.

## Vad man kan göra

- Kurser: lista, lägga till, ta bort (med bekräftelse)
- Studerande: lista, lägga till
- Lärare: lista, lägga till, uppdatera (bland annat kompetensområdet)

## Hur det är byggt

- MVVM med CommunityToolkit.Mvvm. Varje sida har en View (axaml) och en ViewModel.
- `ViewLocator` hittar rätt View till en ViewModel via namnet.
- `PageViewFactory` skapar sidan när man klickar i menyn.
- `HttpService<T>` sköter alla anrop mot restdb (Get, Post, Put, Delete), samma klass som i RunStore.
- Tjänsterna (`CourseServices`, `StudentServices`, `TeacherServices`) har varsitt
  interface i mappen Interfaces och registreras med dependency injection i
  `ServiceCollectionExtensions`.
- Färger, penslar, typsnitt och stilar är återanvända från RunStore. Färger ligger i `Assets/Resources/ColorsResources.axaml`, penslarna i
  `AppResources.axaml` och stilarna i `Assets/Styles`. Allt används som
  `DynamicResource`.

## Köra programmet

1. Skapa en databas på restdb.io med tre collections: `courses`, `students`, `teachers`.
2. Kopiera `Client/appsettings.example.json` till `Client/appsettings.json` och
   fyll i din BaseUrl och ApiKey.
3. `cd Client` och `dotnet run`.

## Skiss

`docs/arkitektur.drawio` visar modellerna, lagren (View, ViewModel, Interface, Service, restdb)
och flödet när en kurs tas bort. Öppnas på https://app.diagrams.net (Arkiv, Öppna från, Enhet).
