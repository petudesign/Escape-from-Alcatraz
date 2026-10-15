# Aloitus: pieni peli, jonka päätökset jäävät talteen

14.10.2026. Tämä on ensimmäinen työskentelyrunko, ei uusi ominaisuuslupaus.

## Lähtökohta

Tavoite on kaupallinen Steam-peli. Tekijä voi käyttää useita tunteja päivässä. Tarkkaa viikkotyömäärää ei vielä ole. Kehityskone on käyttäjän vahvistama Mac mini M6 / 24 GB. Paikallinen tarkistus vahvisti saman; macOS 27.0.1 ja noin 329 GiB vapaata levytilaa tarkistushetkellä. Xcode löytyy Applications-kansiosta. Samassa kansiossa ei löytynyt Unitya, Unreal/Epic Launcheria, Godotia tai Blenderiä; muualla olevia asennuksia ei ole poissuljettu. Editoreita tai buildien toimivuutta ei vielä testattu.

Tekijän tavoite on suunnitella ja toteuttaa peli AI:n avulla. Koodauksen ja 3D-mallinnuksen opettelu eivät ole omia tavoitteita. Tekijän työ on päättää tavoiteltu kokemus, pelata kokeita ja arvioida tulokset. AI:n työ on valmistella toteutus, selittää olennaiset vaihtoehdot ja todentaa toimivuus. AI:n tuottama tiedosto ei yksin todista, että peli toimii.

## Mitä pidetään lähtökohtana?

- Käyttäjän vahvistama: Steam-tuote, tyylitelty grafiikka annetun hahmoreferenssin suuntaan, päätösten ja perustelujen tallennus.
- Aiemman suunnitelman lähtökohtia: 2–4 pelaajan ensimmäisen persoonan vankilapako, vapaa NPC-puhe, sosiaaliset seuraukset ja tallennettava kampanja. Näitä tarkennetaan kokeilla.
- Avoinna: moottori, tarkka miljöö, lopullinen sisältömäärä, hinta, puhepalvelut, julkaisukielet ja julkaisuajankohta.

## Ensimmäinen kokemus

**Kaveri jatkaa pakoreitin valmistelua. Sinä yrität saada vartijan katsomaan muualle.**

Tavoiteltu tunne: yhteinen salaisuus → lähestyvä uhka → improvisoitu pelastus → ymmärrettävä seuraus. Huumori saa syntyä pelaajien ratkaisuista. Vartija säilyttää uskottavan motiivin myös hauskoissa tilanteissa.

Ydinsilmukka: havaitse tilaisuus → sovi työnjako → valmistele jälki → huomaa vartija → harhauta tai peitä → näe seuraus → muuta suunnitelmaa. Roolit voivat vaihtua; toisesta pelaajasta ei tehdä koko pelin ajan puhujaa ja toisesta työntekijää.

## Seuraavat kolme toimitusta

1. **Moottorikoe:** yksi huone, liikkuminen, käytettävä esine, kaksi asiakasta ja mikrofonin lukeminen. Unity ensimmäisenä suosituksena, Unreal vertailussa jos kokeen tai työnkulun ongelma sitä perustelee. Katso [vertailu](ENGINE-EVALUATION.md).
2. **Tilannekoe:** kaksi selliä, peitettävä jälki ja vartijan huomio. Ensin rajattu, toistettava vartijakäytös; sitten puhetulkinta samojen tekojen päälle. Tämä erottaa tilanteen hauskuuden puhepalvelun toimivuudesta. Katso [koesuunnitelma](FIRST-PLAYABLE.md).
3. **Yhteinen pelitesti:** kaksi ihmistä, roolien vaihto, video tai kuvakaappaukset ja havaintomuistiinpanot. Lopputulos on jatko-, korjaus- tai rajauspäätös, ei automaattinen siirtyminen kampanjan tuotantoon.

Ensimmäinen prototyyppi saa olla harmaa. Sen rinnalla pieni hahmon ja sellin ulkoasukoe pitää visuaalisen suunnan mukana. Kokonaista taidepakettia ei tarvitse tuottaa ennen kuin kamerasta ja keskustelutilanteesta on näyttöä.

## Päätösten tallennus

[Päätösrekisteri](decisions/README.md) kertoo nykyisen kannan. [Devlog](devlog/2026-10-14-start.md) kertoo työn etenemisen ja havainnot. [Pohjat](templates/RECORDS.md) auttavat jatkamaan molempia. Jokainen merkittävä muutos yhdistetään kokeeseen ja säilytetään myös silloin, kun idea hylätään.

Case studyssa erotetaan tekijän oma suunnittelupäätös, AI:n ehdotus ja toteutus sekä pelitestissä saatu näyttö. Myöhempi onnistuminen ei muuta tämän päivän oletusta tämän päivän faktaksi.

## Nykyinen tuotelinjauksen lähde

14.10.2026 toimitettu [PROJECT-BRIEF.md](PROJECT-BRIEF.md) on nykyinen briiffi. Ristiriidassa olevat vanhat suunnitelmat ja devlogit kertovat aiemmasta vaiheesta.
