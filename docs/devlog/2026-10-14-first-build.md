# 14.10.2026 — ensimmäinen ajettava Mac-versio

Käyttäjä asensi Unityn, avasi tutoriaalin ja antoi luvan valmistella oman ensimmäisen version poissa ollessaan. Hän toimitti sellireferenssin, saarikartat ja sellirakennuksen leikkauskuvan. AI teki toteutuksen ja automaattiset tarkistukset; käyttäjän kokemusta tai kahden ihmisen pelitestiä ei kirjata niiden perusteella.

## Mitä voi nyt tehdä?

Omassa [Unity-projektissa](../../unity/PrisonCoop/README.md) voi liikkua ensimmäisestä sellistä käytävälle ja toiseen selliin. Pöydän työkalun voi ottaa ja palauttaa. Sellit ovat karuja 2,4 × 3,6 m nimellismoduuleja, joissa on vuode, hylly, pieni pöytä ja yksinkertainen WC. WC:n taakse on varattu suljettu aukko ja seinän taakse huoltotila.

Vapaa NPC-puhe, vartija, verkkopeli, ryömiminen ja aukon työstäminen puuttuvat vielä. Tämä on tilan ja käyttötoiminnon koe, ei valmis vankilapakomekaniikka.

## Päätökset ja syyt

- Koko saaren sijaan aloitettiin kahdesta sellistä. Niillä voidaan tarkistaa ahtaus, käveltävyys ja pakokohdan sijoitus ennen suuren alueen tuotantoa.
- Referenssin vaalea/vihreä seinäjako ja niukka kalustus säilytettiin. Kulumat, WC ja vuode yksinkertaistettiin; ne ovat kehitysmalleja.
- Moottorikoe tehtiin käyttäjän asentamalla **Unity 6000.6.5f1** -editorilla. Aiempi 6.3 LTS -ehdotus ei ollut toteutunut versiolukitus. Input System päivittyi Unityn pakettiratkaisussa versioon 1.20.0; manifest ja lock-tiedosto säilytetään projektissa.
- Ympäristö syntyy koodista, jotta AI pystyy muuttamaan kehitystilaa toistettavasti. Lopullista kenttätyönkulkua tai grafiikkaratkaisua ei vielä valittu.

## Löydetyt viat ja korjaukset

Ensimmäinen build-yritys kaatui tyhjän tallentamattoman kohtauksen rinnalle luotavaan kohtaukseen. Alustus korjattiin valitsemaan uusi yksittäinen kohtaus puhtaassa projektissa ja säilyttämään muokatut käyttäjäkohtaukset.

Ensimmäinen ajotesti löysi työkalun käytöstä virheen: valintasäde osui pelaajan omaan CharacterControlleriin. Pelaaja siirrettiin Ignore Raycast -tasolle; ympäristön törmäykset säilyivät. Uusinta läpäisi saman tavoitettavuustestin. Tämä on esimerkki AI:n tuottamasta koodista, joka tarvitsi oikean ajokokeen ennen onnistumisen kirjaamista.

Renderöintikuvasta huomattiin liian kirkas sellivalo ja katkonainen alaseinän värijako. Valon voimakkuutta pienennettiin ja värialue jatkettiin sivuseinille. Muutos on visuaalinen arvio, ei vielä pelaajapalautteella perusteltu ratkaisu.

## Todisteet

- ARM64 Mac-build valmistui onnistuneesti ja käynnistyi tällä Macilla.
- [Kahdeksan ajotarkistusta](../evidence/2026-10-14/smoke-results.txt) läpäisty; lopullisen testiajon exit code 0.
- [Sellin kamerakuva](../evidence/2026-10-14/cell-view.png) ja [käytävän kamerakuva](../evidence/2026-10-14/corridor-view.png) renderöitiin ajettavasta buildista ja tarkistettiin visuaalisesti.
- Avattu testisovellus nähtiin myös natiivissa ikkunassa ohjeteksteineen. Siinä nähtiin mikrofonin puuttumisen ilmoitus ilman sovelluksen kaatumista. AI ei hyväksynyt mikrofonilupaa tai tallentanut raakapuhetta.
- Buildin Info.plist sisältää mikrofonin käyttökuvauksen. Todellinen puhetasomittaus erillisellä mikrofonilla on vielä avoin.

Lähdekoodi, projektiasetukset, generoitu kohtaus ja assettien metatiedostot säilytetään. Buildit ja Unityn välimuisti ovat versionhallinnan ulkopuolella. Erillistä Git-repoa ei vielä perustettu Notch-repon sisälle.

## Seuraava koe

Käyttäjä arvioi, tuntuuko selli ahtaalta mutta käytettävältä. Sen jälkeen kaksi verkkopelaajaa ja yhteinen työkalun omistajuus; vartijan toistettava käyttäytyminen ennen vapaan puheen kytkemistä. Aukon mitoitus ja WC:n siirtäminen testataan erikseen ennen huoltoreitin laajentamista.
