# Vuosikymmenten käyttöjäljet toimivassa vankilassa

15.10.2026. Käyttäjän pyyntö: tilat saavat näyttää olleen käytössä 30–40 vuotta. Kyseessä on edelleen toimiva vankila. Tarkka käyttöikä on taiteellinen tavoite, ei pinnoista historiallisesti todennettava fakta.

## Taiteellinen ratkaisu

Kuluminen seuraa toimintaa: hankautunutta maalia sängyn ja ovenpielten kohdalla, kengänjälkiä pöydän alla, haaleita lattian naarmuja, portaan käytettyjä etureunoja ja pieniä paljaita metallipintoja oven kosketuskohdissa. Harvat paikkamaalaukset ja vanhat korjausjäljet kertovat jatkuvasta ylläpidosta. Kaikissa selleissä ei ole samaa korjauslaikkua.

Lattian kulkulinjojen sävy ja kiilto vaihtelevat hillitysti. Seinien alareuna ja kosketuskorkeus ovat hieman tummempia. Metalli saa pieniä pintavaurioita, puu kevyttä syyvaihtelua ja kuluneen pöydän reunan. Olemassa oleva lohkeilevan maalin tekstuuri säilyy osana kokonaisuutta.

Vältetty suunta: kaikkialle lisätty paksu lika, oranssi ruoste, laajat kosteusvauriot, sortuneet rakenteet tai tavararöykkiöt. Ne muuttaisivat käyttäjän tarkoittaman käytössä olevan laitoksen hylätyn rakennuksen näköiseksi.

## Toteutus ja modulaarisuus

Materiaalin pintalaji erottaa seinän, lattian, metallin ja puun kulumisen. Age-parametri ohjaa voimakkuutta; se ei simuloi kalenterivuosia. Sijaintiin perustuva shader-vaihtelu ei vaadi uusia ladattavia tekstuureja. Paikalliset hankaus- ja korjausjäljet ovat pieniä staattisia geometrialäiskiä, jotka yhdistetään ympäristön nykyisiin materiaali- ja aluelohkoihin.

Jäljet muodostetaan deterministisesti sellin numerosta. Näin uudelleenrakennus säilyttää saman sellin ilmeen. Jäljet eivät lisää törmäyspintoja tai muuta kulkureittejä. Kulumisrakennus on erillisessä PrisonEnvironment.Wear.cs-osassa; saman toiminnon voi myöhemmin liittää valmiiseen selliprefabiin.

## Tarkistus

Mac-versio 06 ja ajettavan version kuvat: docs/evidence/wear-06. Materiaalien käännös ja ajettavan version kuvat tarkistettiin. Ensimmäisissä kuvissa maalivauriot erottuivat liian vaaleina ja pyöreinä: niiden sävyä hillittiin ja reunaa rikottiin. Pintajäljet eivät heitä omia varjoja. Suorituskykyä ei ole profiloitu; shaderin lisälaskenta ja lisägeometria ovat kustannuksia, joiden lopullinen budjetti on vielä mittaamatta.

Myös lattian hankausjälkien sävyä vaalennettiin lähikuvan jälkeen. Versio 06 läpäisi aiemman reitti- ja kohtaamiskokeen; viimeinen pelkkä materiaalin sävymuutos tarkistettiin kuvakaappausajolla ilman tarpeettomia uusintakokeita.
