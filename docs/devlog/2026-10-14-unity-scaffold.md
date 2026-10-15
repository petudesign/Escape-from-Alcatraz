# 14.10.2026 — ensimmäinen Unity-runko

Käyttäjä pyysi aloittamaan rakentamisen ja tarjoutui asentamaan Unityn. Unity-kokeen aloittaminen on nyt käyttäjän hyväksymä toimintasuunnitelma. Lopullinen tuotantomoottori odottaa kokeen tuloksia.

AI teki [Unity-projektin lähderungon](../../unity/PrisonCoop/README.md): kaksi selliä ja käytävä, ensimmäisen persoonan ohjaus, paikallinen työkalun käteenotto/palautus sekä valinnainen mikrofonin tasotesti. Editorityökalu muodostaa kohtauksen ja tarjoaa Mac-buildin valikkotoiminnon. Ulkoasu käyttää kehitysgeometriaa.

**Design-peruste:** ensin koetaan tila, liike ja esineen käytön palaute. Mikrofoni erotetaan puhetunnistuksesta, jotta laitteen ja käyttöluvan ongelmat voidaan havaita ennen AI-puheketjua. Käyttäjän ei tarvitse rakentaa kohtausta käsin.

**Tekninen rajaus:** ensimmäinen lähderunko käyttää sisäänrakennettua renderöintiä. Alustava URP-suositus siirtyy ulkoasukokeeseen. Verkkopelin tilan ja esineomistajuuden toteutus on seuraava erillinen vaihe; yhden pelaajan runko ei vielä todista co-opia. Koodista generoitu kehityshuone on palautettava koe, ei lopullinen kenttätyönkulku.

**Todentaminen:** lähdetiedostot, pakettilista ja paikalliset dokumenttilinkit on tarkistettu. Unity-editoria tai C#-kääntäjää ei löytynyt, joten käännöstä, pelaamista tai Mac-buildia ei voitu testata. PC-003 jää keskeneräiseksi. Mikrofoniluvan ja rakennuksessa käytettävän shaderin toiminta tarkistetaan aidossa buildissa.

**Seuraava toimitus:** Unity 6.3 LTS -asennuksen jälkeen kirjataan tarkka editoriversio, korjataan mahdolliset käännös- tai ajovirheet ja tehdään ensimmäinen build. Sen jälkeen kaksi verkkopelaajaa samaan tilaan. Moottoriversion lähtömerkintä rungossa on 6000.3.13f1, ei väite uusimmasta korjausversiosta.
