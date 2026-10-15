# Roadmap v0.1

14.10.2026. Aloittava yksintekijä + tekoälyapu; Mac mini M6 / 24 GB, Mac pakollinen. Tekijä ohjaa suunnittelua; koodauksen ja 3D:n opettelu eivät ole erillisiä tavoitteita. Useita tunteja päivässä käytettävissä, tarkka viikkotyömäärä avoin. Päätökset ja kokeiden näyttö kirjataan docs/decisions- ja docs/devlog-kansioihin. Ensin ratkaistaan toteutettavuus ja kiinnostavuus. Kokonaisen kampanjan tuotantoa ei aloiteta ennen näytön saamista.

## Vaiheet ja toimitukset

| Vaihe | Toimitus | Päätös ennen jatkoa |
|---|---|---|
| M0: lähtötilanne | Macin tiedot, työaika, kulukatto, moottorikoe, historiallinen vertailu | Työkalut toimivat omalla Macilla ja työ on palautettavissa versionhallinnasta. |
| M1: pelin ydinkoe | 2 pelaajaa, 2 selliä, vartijatilanne, vapaa puhe, lapun siirto | Harhautus vaikuttaa pelimaailmaan ja kumpikin pelaaja osallistuu. |
| M2: pieni pelattava kokonaisuus | 20–30 min: hankinta → valmistelu → tarkastus → kiinniotto tai pako; 2/3/4 pelaajaa | Kokemus kestää useita ryhmiä ja yhden virheen; kustannus on mitattu. |
| M3: esiteltävä demo | Yksi viimeistelty alue, tunnistettava look, äänet, tallennus/jatkaminen, kulukatot ja tekstisyöte | Ulkopuoliset pelaavat ilman tekijän jatkuvaa ohjausta; rahoitusmateriaali vastaa todellista buildia. |
| M4: kampanjan esituotanto | Sisältöbudjetti, tiimitarve, rahoitus, jakelusuunnitelma | Vasta tässä päätetään 8–12 tunnin tuotannon laajuus. |

## Aikataulun muodostaminen

M0:lle varataan ensin 10–20 keskittynyttä työtuntia. Jos koneessa ei vielä ole työkaluja tai asennukset vaativat opettelua, kirjataan toteuma erikseen. Tämä ei ole valmis peli kahdessa viikossa -lupaus.

M1:n karkea työvaraus on 80–160 tuntia toimivalla perustyöympäristöllä; aloittelijan oppimiseen tarvittavaa aikaa ei voida vielä rajata uskottavasti. Sen jälkeen M2:lle alustavasti 120–240 lisätuntia ja M3:lle 120–240 lisätuntia. Arviot ovat suunnitteluhypoteeseja ja korvataan toteumilla. Tekoälyn kirjoittaman koodin määrä ei ole edistymismittari.

Pelkkien näiden varausten summa M0–M3 on 330–660 h. Lisää 30 % korjaus-/integraatiovarausta: 429–858 h. Esimerkkikalenteri 10 h/vko = noin 43–86 viikkoa, 20 h/vko = noin 22–43 viikkoa. Opettelu, tauot ja palveluntarjoajan ongelmat voivat pidentää tätä. Kahden viikon toteuma tarkentaa aikataulua; ensimmäistä julkaisupäivää ei vielä aseteta.

Koko kampanjan resursointi on avoin. Taide, animaatio, ääni, peliverkko, käsikirjoitus ja QA tarvitsevat osaamista, jota tuotetaan itse tai hankitaan. AI-apu ei tarkoita, että nämä työalueet ovat ilmaisia tai valmiita.

## Ehdotetut mittarit — vahvistetaan M0:ssa

- M1: puheenvuoron lopusta näkyvään, asiayhteyteen sopivaan NPC-reaktioon p50 ≤ 1,5 s ja p95 ≤ 3 s kohdelaitteella ja mitatussa verkkoyhteydessä. Vastauksen aloitusta ja koko puheen valmistumista mitataan erikseen. Nämä ovat tavoitteita, eivät mitattuja arvoja.
- Vartijatilanne: ennalta määritelty 20 tapauksen joukko. Ainakin 18 tapauksessa pelin sääntöjen mukainen tulos; yhtään mahdotonta maailmantilaa ei hyväksytä. Vaihtele sanamuotoja, keskeytyksiä, puhujia ja luottamusta.
- M2: vähintään kolme ulkopuolista ryhmää, mukana 2-, 3- ja 4-pelaajan testi. Kirjataan osallistuminen, jumiutuminen, hauskat tilanteet ja ymmärretyt seuraukset. Pieni otos ei todista markkinakysyntää.
- Yksikään kiinniotettu ei jää pakotetusti pitkäksi aikaa ilman vaikuttavaa toimintaa. Tavoite enintään 60 s toiminnatonta aikaa mitataan.
- M3: vähintään viisi ryhmää läpäisee tai päättää demon ilman tekijän korjauksia. Selvitetään halu jatkaa, mikä jäi mieleen ja missä peli tuntui epäreilulta. Ei johdattelevaa ”ostaisitko tämän?” ainoana näyttönä.
- Kaikki puhekulut raportoidaan sekä lobby-tuntia että pelaajatuntia kohti; havaitaan myös eniten puhuvan ryhmän kulut.

## Tuotteen liiketoimintakysymykset

Premium-kertamaksu on työoletus. Pitkäikäistä pilvipuhetta ei voi luvata rajattomasti ennen talousmallia: per myyty kopio huomioidaan todellinen nettotuotto, kampanjatunnit, uusintapelit, NPC-keskustelut, tuki ja palvelun käyttöikä. Tarkkaa hintaa ei lukita.

Julkaisualustojen ja kielten määrä ratkaistaan demotuloksilla. Mac on mukana alusta, Windows on kaupallisen jakelun selvitettävä kohde. Suomenkielisen puheen toimivuutta ei päätellä englanninkielisestä testistä.

Rahoitusta varten tarvitaan oikea pelivideo, toimiva demo, tekijän osaamis- ja tiimisuunnitelma, rajattu tuotantobudjetti ja mitattu pilvikulu. Kaupalliseen esittelyyn ei käytetä ilman lupaa muiden teosten hahmoja, musiikkia tai muuta materiaalia. Oikeuksien tarkistus on erillinen tehtävä.
