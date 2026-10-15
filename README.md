# Vankilapako — tuotesuunnitelma v0.3

Päivitetty 14.10.2026. Työnimi, ei lopullinen nimi. Tämä kansio sisältää suunnittelua, tutkimusta ja ajettavan Unity-prototyypin. Nykyiset tuotelinjaukset: [projektin briiffi](docs/PROJECT-BRIEF.md). Dokumenttien ehdotukset eivät ole lukittuja tuotelupauksia.

Aloita tästä: [projektin aloitus ja työskentelytapa](docs/START-HERE.md). [Päätösrekisteri](docs/decisions/README.md) ja [ensimmäinen devlog](docs/devlog/2026-10-14-start.md) säilyttävät perustelut myöhempää case studyta varten.

[Modulaarisuuden nykyinen rakenne ja jatko](docs/MODULARITY.md). [Suorituskyky ja koko](docs/PERFORMANCE.md).

Nykyinen visuaalinen versio: [kolmen kerroksen selliosasto, valvontahalli, ruokala ja suurempi piha](docs/VISUAL-START.md). Mac-sovellus: `unity/PrisonCoop/Builds/macOS/PrisonCoop-10.app`. Asetukset säilyvät; paikallinen hahmonäkyvyyden verkkokoe on käytettävissä H/J-näppäimillä. NPC-maailmaa ei vielä synkronoida. [Visuaalisen työn devlog](docs/devlog/2026-10-14-visual-prison.md).

Aiempi [Unity-kokeen ohje](unity/PrisonCoop/README.md) säilyy teknisen vaiheen dokumenttina. [Alustava vankilan rakenne](docs/PRISON-LAYOUT.md) tarkentuu visuaalisen version perusteella.

## Tuote yhdessä lauseessa

2–4 pelaajan ensimmäisen persoonan co-op-vankilapeli, jossa valmistellaan yhteistä pakoa 1960-luvun vankilasta, puhutaan vapaasti tekoälyhahmoille ja selvitetään omien sekä kavereiden ratkaisujen seurauksia.

## Lähtötiedot

- Tekijä aloittaa yksin ja haluaa suunnitella sekä toteuttaa pelin AI:n avulla. Koodauksen tai 3D-mallinnuksen opettelu ei ole oma tavoite. Tekijä ohjaa kokemusta ja arvioi tulokset; AI auttaa toteutuksessa, tarkistuksissa ja ratkaisujen perusteluissa.
- Mac on pakollinen kehitys- ja ensimmäinen testialusta. Windows-tuki tutkitaan ennen laajempaa kaveritestausta.
- Käyttäjä voi käyttää useita tunteja päivässä. Mac mini M6 / 24 GB on vahvistettu; tarkka viikkotyömäärä ja rahabudjetti ovat avoimia. Kalenteripäivämääriä ei luvata ennen ensimmäistä toteutuskokeilua.
- Tavoite on kaupallinen Steam-tuote. Tyylitellyn hahmon lähtöreferenssi ja sen tulkinta on säilytetty [taidesuunnassa](docs/ART-DIRECTION.md).
- Tavoiteltu valmis kokemus: usean peli-illan tallennettava kampanja; tavoiteltu 8–12 tunnin ensimmäinen läpipeluukokemus on hypoteesi, ei sisältömääräys.
- Miljöö: 60-luvun saarivankila. Oma maailma ja lore, vahvasti Alcatrazista inspiroitu.

## Mikä tekee pelistä tunnistettavan?

1. Pelaajat puhuvat omin sanoin vartijoille ja vangeille. Puhe saa pelillisiä seurauksia.
2. Eri pelaajilla on eri tietoa ja mahdollisuuksia. Laput, kuiskaukset ja harhautukset yhdistävät ryhmää.
3. Virhe avaa vaikeamman tilanteen. Rahojen häviäminen voi synnyttää velan; kiinnijääminen uuden tehtävän.
4. Vankilan sosiaalinen arki rakentaa yhteistä historiaa ennen lopullista pakoa.
5. Persoonallinen, tyylitelty ulkoasu ja tunnistettava äänimaailma. Huumori syntyy tilanteista.

Kysyntä on vielä todentamatta. Sisällöntuottajapotentiaali ja oma innostus ovat lähtöhypoteeseja, eivät myyntiennuste.

## Aiempi ensimmäisen järjestelmäkokeen suunnitelma

Kaksi pelaajaa, kaksi selliä, yksi käytävä, vartija ja yksi vanki. Kaveri työstää jälkeä, vartija huomaa sen ja toinen pelaaja yrittää pelastaa tilanteen vapaalla puheella. Vaihtoehtona lappu ja valmisteltu harhautus. Kesto 10–15 min. Kehitysgrafiikka riittää.

Macilla pitää saada kaksi verkkopeliasiakasta samaan tilaan. Pelkkä tekstikeskusteludemo ei vielä todista puheeseen perustuvaa co-op-peliä. Toinen oikea testaaja tarvitaan; tekoälyavustaja ei korvaa sitä.

Ensimmäisestä kokeesta jätetään pois koko saari, kampanja, julkinen matchmaking, CPU-pakokaverit, tappaminen, blackjack ja leaderboardit. Ne säilyvät myöhemmässä backlogissa.

## Pelisäännöt, jotka säilytetään

- 2–4 pelaajan tuki rakennetaan ilman pakollisia tekoälyjoukkuetovereita; ensin 2, sitten testit myös 3 ja 4 pelaajalla.
- NPC tietää vain omat havaintonsa ja sille välitetyt asiat. Ei kaikkitietävää yhteistä muistia.
- Kieli on vapaa; pelimaailman toiminnot ja seuraukset ovat rajattuja ja validoituja.
- Ei pelaajille näkyviä NPC:n suunnitelmamittareita. Varoitukset tulevat havainnoista, puheista ja lapuista; tarpeen mukaan saavutettavat tekstitykset ja loki.
- Kiinniotettu pysyy pelaajana: kuulustelu, viivytys tai muu vaikuttava tehtävä. Pakollinen pitkä katsominen ei ole suunnitelma.
- Ei yhteistä rahasaldoa: henkilökohtainen prison account, fyysiset vaihtotavarat ja sosiaalinen talous.
- Uhkaavat ja väkivaltaiset tilanteet käsitellään fiktiivisinä pelitoimintoina. Palveluntarjoajan soveltuvuus tälle sisällölle testataan ennen valintaa; rajoituksia ei yritetä kiertää.

## Tekniikan työoletus

Nykyinen toteutus käyttää Unitya. Lopullinen tuotantomoottoripäätös on erillinen. AI:n päivitetty suositus on Unity ensimmäiseen kokeeseen; Unreal on keskeinen vertailuvaihtoehto ja Godot säilyy aiemmasta suunnitelmasta vaihtoehtona. Suositus perustuu AI:n avustamaan työnkulkuun, ei vielä mittaustuloksiin. Ratkaisevat testit: Mac-build, mikrofoni, kaksi verkkopelaajaa ja toteutuksen korjattavuus. Katso [moottorivertailu](docs/ENGINE-EVALUATION.md). Moottoria ei valita mahdollisen apurahan perusteella.

Puheketju: mikrofoni → puhe tekstiksi → NPC:n kuultu tieto + rajattu muistiyhteenveto → kielimallin tulkinta → pelin hyväksymä teko → hahmon puhe. Host ylläpitää pelitilaa. Palveluntarjoajan avaimet pysyvät omassa taustapalvelussa; niitä ei jaeta pelin mukana. Julkinen demo vaatii myös käytön rajat ja häiriötilan.

## Dokumentit

- [Myöhemmän vankilamaailman do/don’t-muistio](docs/ENVIRONMENT-GUIDELINES.md)
- [Valmiiden assettien käyttö ja ensimmäinen shortlist](docs/ASSET-STRATEGY.md)

- [Aloitus ja tekijän työskentelytapa](docs/START-HERE.md)
- [Taidesuunta ja hahmoreferenssi](docs/ART-DIRECTION.md)
- [Unity / Unreal / Godot ja moottorikoe](docs/ENGINE-EVALUATION.md)
- [Ensimmäisen pelitilanteen koesuunnitelma](docs/FIRST-PLAYABLE.md)
- [Päätösrekisteri](docs/decisions/README.md)
- [Devlog: 14.10.2026](docs/devlog/2026-10-14-start.md)
- [Päätös-, devlog- ja pelitestipohjat](docs/templates/RECORDS.md)
- [Steam-tuotteen lähtökohdat](docs/STEAM-PRODUCT.md)

- [Roadmap ja päätösportit](docs/ROADMAP.md)
- [Paikka ja historiallinen tutkimus](docs/SETTING-RESEARCH.md)
- [Puhetekoälyn kustannukset](docs/AI-COSTS.md)
- [Rahoituspolut](docs/FUNDING.md)
- [ChatGPT:lle kopioitava kuvaus](docs/SECOND-OPINION.md)
- [Toteutettavat ticketit](planning/BACKLOG.md)
- [Muokattavat kustannusoletukset](planning/voice-cost-inputs.json)
- [Laskennan tulokset](planning/voice-cost-results.json)

PC-001 on osittain selvitetty: kone ja työskentelytapa on kirjattu, mutta kulukatto ja testikaveri ovat avoimia. Unityn ensimmäinen paikallinen tilakoe on ajettava ja testattu. Asetukset ja PC-005:n kahden asiakkaan yhteinen tila on toteutettu ja tarkistettu saman Macin erillisissä prosesseissa. Kahden ihmisen pelitilanne ja yhteys eri koneiden välillä ovat avoimia; mikrofonikoe on käyttäjän toiveesta siirretty myöhemmäksi. PC-002:n erillinen versionhallinta on vielä ratkaisematta. Nykyinen kansio kuuluu Notch-projektin Git-juureen; erillistä pelirepoa ei ole perustettu. Maksullisia palveluita ei ole kytketty, tilejä perustettu eikä rahoitushakemuksia lähetetty.
