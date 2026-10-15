# Unity vai Unreal — päätöstä edeltävä koe

14.10.2026. **Suositus: aloita Unity-kokeella. Moottoria ei ole valittu.** Godot säilyy aiemmasta suunnitelmasta kolmantena vaihtoehtona. Pelkän vertailun vuoksi ei rakenneta kolmea prototyyppiä.

## Mikä ratkaisee tässä pelissä?

Macilla toimiva päivittäinen työnkulku, kahden pelaajan sama maailmantila, mikrofonista NPC:n tekoon kulkeva ketju ja toteutuksen korjattavuus AI:n avulla. Käyttäjä haluaa ohjata suunnittelua ja ymmärtää perustelut. Editorin solmujen tai koodin manuaalinen rakentaminen ei ole käyttäjän tavoite.

| Vaihtoehto | Miksi harkita? — suunnitteluarvio | Mitä kokeessa pitää todentaa? |
|---|---|---|
| Unity, C# ja alustavasti URP | Tekstimuotoiset skriptit sopivat AI:n muutoksiin ja tarkistuksiin. Tyylitellyn pienen 3D-alueen vaatimus ei lähtökohtaisesti edellytä raskasta renderöintiä. | Editorin asetusten toistettavuus, mikrofoniluvat, verkko, animaatiotuonti ja Mac-build. AI:n koodiapu ei automaattisesti hallitse editoritilaa. |
| Unreal, Blueprints ja tarvittaessa C++ | Sisäänrakennettu verkkopelin rakenne on relevantti co-opille. Blueprint voi tehdä käyttäytymisen hahmottamisesta visuaalista. | Editorin ja kahden asiakkaan muistikuorma, Xcode-yhteensopivuus sekä AI:n kyky tuottaa, muuttaa ja tarkistaa tarvittavat assetit. Visuaalinen ohjelmointi ei itsessään vähennä käyttäjän työtä. |
| Godot | Säilyy vertailussa, jos Unityn tai Unrealin työskentelypolku ei sovi. Aiempi README ehdotti sitä ennen tämän keskustelun tarkennusta. | Sama puhe-/verkko-/build-koe ja myöhempien tarvittavien liitäntöjen saatavuus. Pieni editori ei yksin osoita pienintä kokonaiskehitystyötä. |

Suositus Unitysta on työnkulkuhypoteesi, ei mitattu paremmuus. Unreal voi hyvin sopia tyyliteltyyn peliin. Jos Unrealin työnkulku osoittautuu omassa kokeessa sujuvammaksi, suositus muutetaan. Moottorin valinta ei ratkaise AI-puheen kustannuksia tai viivettä.

## Tarkistetut tekniset lähtötiedot

- Unityn 6.3-dokumentaatio kuvaa Apple Silicon- ja Metal-tuen macOS:llä; IL2CPP vaatii Xcoden. Tämä ei todista tämän koneen buildia. [Unity: macOS](https://docs.unity.com/en-us/engine/6000.3/manual/platform-specific/apple-mac/macos-requirements-and-compatibility)
- Unity tarjoaa Netcode for GameObjects -paketin. Tarkka editori-/pakettiversio valitaan ja kirjataan yhdessä; pelitilan synkronointi tarvitsee toteutuksen. [Unity Netcode](https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@3.1/index.html)
- Unrealin Mac-ohje suosittelee vähintään 32 GB muistia ja ilmoittaa minimiksi 16 GB. Käytössä oleva 24 GB jää niiden väliin; sujuvuutta ei voi päätellä RAM-määrästä. Tarkka moottori-/Xcode-pari tarkistetaan ennen asennusta. [Epic: Mac-vaatimukset](https://dev.epicgames.com/documentation/en-us/unreal-engine/macos-development-requirements-for-unreal-engine)
- Unreal käyttää verkkopelissä palvelimen hallitsemaa tilaa ja tarjoaa replikaation. Pelikohtaiset esineet, NPC-tilat ja äänen tapahtumat tarvitsevat silti toteutuksen. [Epic: Networking overview](https://dev.epicgames.com/documentation/en-us/unreal-engine/networking-overview-for-unreal-engine)
- Godotissa on korkean tason moninpelirajapinta. [Godot: multiplayer](https://docs.godotengine.org/en/stable/tutorials/networking/high_level_multiplayer.html)

Lisenssit, kaupalliset ehdot ja tarvittavat lisäosat tarkistetaan ennen lopullista päätöstä. Tässä ei vertailla hintoja eikä päätetä maksullisista hankinnoista.

## Sama pieni koe ehdokkaille

Yksi 3D-huone → liikkuminen → esineen käyttö → kaksi paikallista asiakasta → molemmille sama esineen tila → mikrofonilupa ja äänitason havainto → itsenäinen Mac-build. Mikrofoni tässä ei vielä tarkoita toimivaa puhetunnistusta tai proximity-chatia.

Kirjaa asennus- ja aktiivinen työaika erikseen, editorin ja asiakkaiden muistikuorma, buildin käynnistyminen, törmätyt ongelmat sekä AI:n vaatima manuaalinen apu. Suorituskykytavoite kokeeseen: 60 fps pienessä huoneessa 1080p:llä; tavoite on ehdotus, ei tulos tai lopullinen minimilaitevaatimus.

Kokeelle ehdotettu aktiivisen työn aikaraja on 6–10 h asennusten lisäksi. Se on tutkimusbudjetti, ei lupaus valmistumisesta. Jos aikaraja ylittyy, kirjataan este ja arvioidaan vertailuehdokas ennen uusien järjestelmien lisäämistä. Jos Unity läpäisee kokeen ja työnkulku tuntuu hyvältä, Unrealin täysi rinnakkaistoteutus ei ole välttämätön.

Lopulliseen päätösmerkintään tulevat versiot, oikea build, kokeen tulokset, vaihtoehdon hylkäysperuste ja hyväksytyt haitat. Julkaisu Windowsille testataan Windows-koneella myöhemmässä vaiheessa ennen julkaisulupausta.
