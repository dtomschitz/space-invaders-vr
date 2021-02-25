# Space Invaders
VR Space Invadors ist eine Neuinterpretation des Shoot-'em-up Games "Space Invaders", das 1978 von Tomoshiro Nishikado entwickelt wurde. Der Spieler steuert eine Kanone, mit dem er in einer virtuellen Welt Gegner abwehren muss.

## Gameplay
Während der Spieler sich über das ganze Spiel statisch in einem Cockpit befindet, werden Gegner im All erscheinen und auf den Spieler schießen. Treffen die Schüsse den Spieler, verliert er an Lebenspunkten; fallen diese auf Null, verliert der Spieler und muss von vorne spielen. Der Spieler kann sich wehren, indem er einen Schild aktiviert oder mit den Geschütztürmen die Gegner eliminiert. 

### Spielmechaniken

#### Geschütztürme
Der Spieler steuert die Geschütztürme mit beiden Händen: Den linken Turm mit der linken Hand, den rechten Turm mit der rechten Hand. Durch das Drücken des Triggers schießt der Spieler und kann damit Gegner erledigen.

#### Lebenspunkte
Der Spieler verliert Lebenspunkte, wenn die Gegner auf den Spieler schießen und der Spieler seinen Schild nicht rechtzeitig aktiviert oder die Energie zum Aktivieren fehlt. Mit den Geschütztürmen kann der Spieler die Gegner eliminieren, bevor sie auf ihn schießen können. 

#### Der Schild
Durch Drücken des roten Buttons im Cockpit wird der Schild aktiviert, der Angriffe der Gegner absorbiert. Solange der Schild aktiviert ist, kostet es Energie, um ihn aufrechtzuerhalten. Fällt die Energie auf null, bricht der Schild zusammen und der Spieler muss warten, bis der Schild wieder genügend Energie hat, um ihn zu aktivieren.

#### Die Gegner
Die Gegner erscheinen vor dem Spieler an zufälligen Positionen im Weltraum. Sie können den Schüssen der Geschütztürme ausweichen und auf den Spieler selbst schießen. Es werden nie mehr als 3 Schüsse gleichzeitig auf den Spieler abgefeuert. Trifft der Spieler sie mit seinen Schüssen, werden sie zerstört.

#### Wellen
Die Gegner erscheinen in Wellen, um die Spawnlogik einfacher zu gestalten. Die Spieler selbst hat keine Übersicht, in welcher Welle er sich gerade befindet, aber der Übergang zwischen den Wellen ist beinahe fließend. Nachdem der Spieler alle Gegner einer Welle getötet hat, starten kurz darauf die nächste Welle. Eine Welle hat immer eine maximale Anzahl an Gegner, die spawnen können.

#### Statistiken
Verliert der Spieler, dann werden ihm seine Statistiken zu der verlorenen Runde angezeigt. Gezählt werden die Anzahl der eliminierten Gegner und die Zeitlänge, die der Spieler überlebt hat.

## Level Design

### Der Weltraum
Der Weltraum besteht aus einem 2D-Image und Planetenmodelle, die als Fixpunkte für den Spieler dienen. Der Erscheinungsbereich der Gegner befindet sich vor den Planeten.

### Das Cockpit
Der Spieler sitzt in einem Stuhl innerhalb eines kleinem Raumschiffes. Das Raumschiff hinter dem Stuhl eine Türe und LED Beleuchtung an den Wänden eingebaut. Vor dem Spieler befinden sich drei Holobildschirme: Der linke gibt die Anzahl eliminierter Gegner an, der rechte die überlebte Zeitlänge und der mittlere die Lebenspunkte und die Schildenergie. Neben den Holobildschirmen befindet sich ein roter Knopf, der deim Betätigen den Schild aktiviert. Vor dem Spieler sind noch zwei Hebel angebracht, die einzig und allein als Designelement dienen und keine Aufgabe übernehmen. Die Front des Schiffes ist einer Glaskuppel ähnlich, um den Spieler einen größtmöglichen Blick ins Weltall zu geben.

### Der Schild
Der Schild befindet sich wie eine Kugel um das Schiff herum und wenn er aktiviert wird, sieht der Spieler gelbe Honigwaben entlang der Kugel vorbeiziehen.

## Stil

### Space Invaders Classic
Der Stil des Spiels ist überwiegend dem klassischen Spiel angelehnt. Planeten, Weltraum, Schussanimation sowohl der Spieler, als auch der Gegner und das Cockpit sind eigeninterpreationen des Teams.

### Sound und Musik 
(David) du weißt, wo du deine Sounds her hast. Ich nicht :D

## User Interface und Steuerung

### Hauptmenü
Das Hauptmenü besteht aus den folgenden Elementen:
- Spiel starten
- Spiel beenden 

### Pausemenü
(David)

### HUD
Das HUD ist im Schiff statisch integriert und befindet sich nicht klassisch am Sichtfeld des Spielers, um Motionsickness zu vermeiden. Es besteht aus folgenden Elementen:
- Lebensanzeige
- Schildenergieanzeige
- Eliminierungszähler
- Zeitmesser

### Steuerung
Das Blickfeld des Spielers wird durch die Position der VR-Brille gesteuert; das gleiche gilt auch für die Geschütztürme, nur das hier die Position der Controller gemessen wird. Mit den beiden Trigger kann der Spieler schießen. (David, du kennst die Steuerung und die Controller, schau hier mal drüber bitte)

## Models
Alle Models werden als dreidimensionalen Objekte in Blender 2.8 modelliert. Die Models
werden mit einer einfach gehaltenen Textur überzogen, die aus wenigen Basisfarben
besteht. Die Größe der Models sind aufeinander abgestimmt, sodass es im späteren
Gesamtbild zu sinnvollen Größenverhältnissen kommt. Im Folgendem werden alle Models
kurz aufgelistet.

- Weltraumhintergrund
- Fläche, auf dem die Holobildschirme platziert sind
- Holobildschirme
- Die Hebel
- LED-Lampen für Innenraumbeleuchtung
- Modelle für Aliens
- Die drei Planeten
- Das Cockpit
- Die Geschütztürme
- Der Stuhl

## Teamrollen
- **Christof Schwarzenberger**: Modellierung/Level Designer
- **David Tomschitz**: Gameplay Programmierung
- **Duane Englert**: Game Design/Gameplay Programmierung
- **Sundar Arz**: KI Programmierung/Gameplay Programmierung
