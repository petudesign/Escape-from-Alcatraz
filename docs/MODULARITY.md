# Modulaarisuus ensimmäisestä näkymästä eteenpäin

15.10.2026. Käyttäjä haluaa viimeistellä ensimmäisen näkymän ja säilyttää mahdollisuuden laajentaa peliä. Tämä dokumentti erottaa nykyiset rajapinnat myöhemmistä tarpeista.

## Mitä erotettiin nyt?

| Osa | Vastuu | Mitä voi vaihtaa sen ulkopuolella? |
|---|---|---|
| PrisonerAppearance, neljä Unity-assettia | Mitat, kasvovariantti, iho, hiukset ja vaatteiden värit | Ulkoasua voi muuttaa koskematta reitteihin tai törmäyksiin. |
| PrisonerVisual | Geometria ja liikkeen perusteella animoitu asento | Myöhemmin rigattu malli ja animaatiot voivat korvata tämän komponentin. |
| PrisonerMotor | Kävely, törmäykset, lähietäisyyden väistö ja nykyinen reitti | Päivärytmi voi antaa uuden reitin SetRoute-kutsulla. |
| WalkingPrisoner | Yhdistää ulkoasun ja liikkeen | Ei sisällä keskusteluja, taloutta tai vankilan päivärytmiä. |
| PrisonPopulation | Nykyisen kokeen hahmojen ja reittien sijoittelu | Myöhemmin sijoittelut ja päivän tehtävät voidaan lukea omasta datasta. |

Reittikävely on paikallinen ratkaisu tunnetulle alueelle. Se ei etsi reittiä useiden huoneiden, lukittujen ovien ja kerrosten läpi. Kun työvuorot ja ovet tulevat mukaan, tarvitaan varsinainen kulkukelpoinen navigointiverkko sekä tieto pääsyoikeuksista. Siihen ei kannata piilottaa NPC:n tietoa tai keskustelumuistia.

## Laajennus seuraavassa pelitilanteessa

Ensimmäiseksi kannattaa valita yksi pelattava tilanne, esimerkiksi selli → työvuoro → palvelus → hyödyllinen tieto. Sille tehdään pieni toimiva ketju, josta saadaan seuraavat todelliset tarpeet. Tämä on AI:n ehdotus, ei koko kampanjan lukittu toteutusjärjestys.

- Päivärytmi antaa NPC:lle tehtävän ja paikan. Motor toteuttaa kulun, visual näyttää liikkeen.
- Esineen määrittely kertoo, mitä se mahdollistaa. Pelaajan omistama esine ja sen sijainti ovat erillistä pelitilaa.
- NPC:n identiteetti, tieto ja muisti pysyvät erillään ulkoasusta. Sama vaatetus ei tarkoita samaa henkilöä.
- Keskustelu ehdottaa rajattua tekoa, jonka pelisäännöt hyväksyvät. Se ei siirrä esineitä tai rahaa suoraan.
- Tili, vaihtotavarat ja velat ovat omia tilojaan. Ei yhteistä rahasaldoa.
- Verkkopelin auktoriteetti ja tallennus liitetään hyväksyttyihin pelitapahtumiin; nykyinen visuaalinen koe ei ole valmis verkkopeliarkkitehtuuri.

## Ympäristön nykyinen raja

Sellit, kalusteet, kaiteet ja valaisimet ovat tällä hetkellä lähdekoodin rakennusmoduuleja, joista tallennetaan Unity-kohtaus ja yhdistetty geometria. Tämä auttaa tuottamaan nykyisen alueen nopeasti. Se ei vielä ole suunnittelijalle valmis prefab-kirjasto.

Kun sellin mittakaava ja ilme hyväksytään, selli, ovikehys, kalusteet ja käytäväjakso kannattaa irrottaa erillisiksi prefabeiksi. Muuttuvat ovet, venttiilit ja tavarat tarvitsevat omat identiteetit; niiden toiminnallisia osia ei yhdistetä staattiseen seinägeometriaan. Koko saaren generaattoria ei tarvita ensimmäisen näkymän viimeistelyyn.

## Päätöksen peruste

Modulaarisuuden hyöty on muutoksen rajaaminen: yhden hahmon paidan, yhden oven tai yhden työvuoron muuttaminen ei saa vaatia koko pelin uusimista. Kaikkea tulevaa ei voi ennakoida. Lisäämme eroteltavia järjestelmiä todellisen pelitilanteen tarpeesta ja säilytämme pienet ajettavat versiot sekä päätösten perustelut.

## Version 07 rakennusmuutos

Kolmen kerroksen selliosasto, porras, valvontahalli, ruokala ja piha rakentuvat PrisonEnvironment.Architecture.cs-osassa. Solun kalusteet ja pintakuluma säilyvät omissa osissaan. TierHeight, CellTiers ja CellsPerRow ovat näkyviä rakennuksen mittoja; ne eivät vielä muodosta yleiskäyttöistä vankilageneraattoria. Uusi porraskulkukoe käyttää oikeaa pelaajan törmäyskapselia ja kirjattua reittiä.
