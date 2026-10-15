# Ensimmäisen näkymän viimeistely ja kohtaamisjumin korjaus

15.10.2026. AI-avustajan toteutusmerkintä, käyttäjän palautteen perusteella.

## Lähtötilanne

Käyttäjä halusi parempaa grafiikkaa ja paremmat hahmot ennen uusien pelijärjestelmien laajentamista. Lisäksi hän havaitsi hahmojen kävelevän toisiaan päin ja jäävän jumiin. Version 04 lyhyt liikehavainto ei testannut kohtaamisia ja oli liian rajattu näyttö liikenteen toimivuudesta.

## Mikä muuttui?

- Molemminpuolinen pysähtyminen korvattiin ennakoivalla paikallisella väistöllä. Vangit suosivat omaa oikeaa puoltaan ja arvioivat vaihtoehtoisia kulkusuuntia. Ympäristöä tarkistetaan kapselipyyhkäisyllä ja lattian jatkumista alas suuntautuvalla säteellä. Törmäykset säilyvät; hahmoja ei siirretä toistensa läpi.
- Pitkittynyt este voi vaihtaa seuraavaa reittipistettä. Tämä on rajattu palautumissääntö nykyisille tunnetuille reiteille, ei yleinen polunhakuratkaisu.
- Hahmon ulkoasu, animaatio ja kävely erotettiin. Neljä ScriptableObject-assettia säilyttää ulkoasun mitat ja värit. [Modulaarisuuden vastuut ja rajat](../MODULARITY.md).
- Hahmojen hartioita, kauluksia, lantiota, kasvojen tasoja, silmiä, käsiä ja kenkiä tarkennettiin. Vertex-värit muunnetaan oikeaan väriavaruuteen, jotta ihot ja vaatteet eivät jää aiemman haaleiksi. Hahmot ovat yhä ohjelmallisesti muodostettua faceted-geometriaa, eivät konseptikuvan tasoisia rigattuja tuotantohahmoja.
- Jäykät pienosat yhdistetään nivelkohtaisiksi mesheiksi. Tämä vähentää erillisten renderöijien määrää. Suorituskyvyn parannusta ei ole vielä mitattu profilerilla.
- Seinien kuluneisuutta hillittiin, alaseinän sävyä syvennettiin ja käytävän valaisimiin lisättiin varjot. Putket ja valaisinten suojukset tarkentavat rakennuksen mittakaavaa. Kuvakatselmoinnissa poistettiin oviaukkojen yli ulottuva koristelista ja liian suuri valaisimen kattovarjo sekä käännettiin väärältä puolelta luettu B BLOCK -kyltti.

## Miksi tämä rajaus?

Näyttävä ensimmäinen selliosasto ja siinä liikkuvat hahmot antavat konkreettisen pohjan kokemuksen arviointiin. Päivärytmiä, vapaata puhetta ja taloutta ei lisätty tähän korjaukseen. Niiden kannattaa myöhemmin ohjata erillisiä toimintoja saman hahmon sisällä. Koko tulevan saaren navigointia ei kannata tehdä nykyisten suorien reittien sisään.

[Unityn CharacterController-kuvaus](https://docs.unity.com/en-us/engine/6000.6/script-reference/unityengine/charactercontroller) toimii käytetyn liikkumiskomponentin API-lähteenä. Käytännön kohtaamistulos perustuu omaan ajettavaan kokeeseen, ei API-dokumentista tehtyyn oletukseen.

## Tarkistusnäyttö

Unity 6000.6.5f1:llä rakennettu Mac-versio 05. Ajettavan sovelluksen kuvat: [selliosasto](../evidence/polish-05/cellblock.png), [hahmo](../evidence/polish-05/character.png), [selli](../evidence/polish-05/cell.png). Shaderien ja C#-käännöksen virheet tarkistettiin build-lokista.

Automaattinen sisäinen kuvakaappausajo sisältää:

1. Kaikkien seitsemän vangin siirtymä kolmen sekunnin aikana.
2. Tavallisten risteävien reittien 45 lisäpelisekunnin ajo. Jokaisen pitää saavuttaa reittipisteitä. Reittipisteet ja pisin pysähtyminen kirjataan routes.txt-tiedostoon.
3. Pakotettu vastakkainen kohtaaminen samalla linjalla. Seurataan ohitusta, pienintä keskinäistä etäisyyttä ja pysähtymistä. Tulos encounter.txt-tiedostossa.

Tarkistustiedostot ovat docs/evidence/polish-05-kansiossa. Kyseessä on lyhyt paikallinen koe nykyisessä tilassa, ei takuu kaikista ruuhkista, uusista huoneista tai ovitilanteista. Testi ei ota kantaa verkkoon, mikrofoniin, kampanjan hauskuuteen tai lopulliseen suorituskykyyn. Lopullinen visuaalinen hyväksyntä kuuluu käyttäjälle.

Lopullisen ajon tulos: kaikki seitsemän liikkuivat, tavallisilla reiteillä saavutettiin 3–10 reittipistettä per hahmo, ja pisin tahaton pysähdys oli 0 sekuntia. Pakotettu ohitus onnistui; pienin hahmojen keskipisteiden välinen etäisyys oli noin 0,862 m. Sovellus poistui koodilla 0.
