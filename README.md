<p align="center">
    <img src="Docs/Screenshots/Poster.png">
    <span style="font-weight: bold">Hinweis: Das Spiel wurde nur mit der ersten Version der Oculus Rift getestet!</span>
</p>

Space Invaders VR ist eine Neuinterpretation des Shoot-'em-up Games "Space Invaders", das 1978 von Tomoshiro Nishikado entwickelt wurde. Der Spieler sitzt in einem Raumschiff und steuert zwei Geschütze, mit denen er in einer virtuellen Welt Gegner in verschiedenen Wellen abwehren muss. Der Stil des Spiels ist überwiegend dem klassischen Spiel angelehnt. Planeten, Weltraum, Schussanimation sowohl der Spieler, als auch der Gegner und das Cockpit sind eigene Interpretationen des Teams.

# Gameplay

Der Spieler befindet sich über das ganze Spiel hinweg in dem Cockpit eines Raumschiffes und muss versuchen so viele Gegner wie möglich zu besiegen. Diese erscheinen in unterschiedlichen Intervallen und versuchen den Spielern mit ihren Schüssen zu Töten. Um dies zu verhindern muss der Spieler, die verschiedenen Gegner entweder schnell Abschießen oder im Notfall sein Schutzschild aktivieren, welches ihn vor den Einschlägen schütz. Besonders das Schutzschild muss vom Spieler regelmäßig genutzt werden, da Lebenspunkte nicht regeneriert werden können und das Spiel endlos Gegner enstehen lässt. Sollte der Spieler alle Lebenspunkte verloren haben, was mit längerer Spielzeit immer wahrscheinlicher wird, muss das Spiel von vorne Begonnen werden. Ziel des Spiels ist es, so viele erfolgreiche Abschüsse wie Möglich zu erreichen.

# Spielmechaniken

## Die Geschütztürme

Der Spieler steuert die Geschütztürme jeweils mit dem linken und rechten Controllern und kann mittels des Trigger-Buttons einzelne Schüsse abgeben. Um das Zielen zu erleichtern, sieht der Spieler jeweils zwei Fadenkreuze, welche sich in die Zielrichtung positioniert haben. Durch die Bewegung des jeweiligen Controllers, wird das dazugehörige Geschütz mit bewegt bzw. rotiert. Jeder erfolgreiche Schuss, tötet einen Gegner direkt und der Kill wird dem Spieler in seiner Statistik gutgeschrieben.

## Lebenspunkte

Wird der Spieler von einem Gegner getroffen, kostet dies Lebenspunkte, kann aber durch geschicktes aktivieren und deaktiveren des Schutzschildes verhindert werden. Die Kombination aus schnellem Reagieren und Schießen ist zwingend notwendig, wenn der Spieler lange überleben möchte, denn Lebenspunkte können nicht regeneriert werden. Hat der Spieler alle seine Punkte aufgebraucht, wird das Spiel beendet und die erspielte Statistik zurückgesetzt.

## Das Schutzschild

Damit der Spieler von Gegner nicht getroffen werden kann, muss das Schutzschild durch drücken des roten Schalters im Cockpit aktiviert werden. Der Spieler muss jedoch dies mit bedacht handhaben, da das Schutzschild nur eine begrenzte Energie zur verfügung hat. Ist diese Aufgebraucht, wird das Schild deaktiviert und kann erst wieder aktiviert werden, wenn genug Energie angespart wurde. Treffer der Gegner kosten keine zusätzliche Energie, während das Schutzschild genutzt wird.

## Die Gegner

Die Gegner erscheinen vor dem Spieler an zufälligen Positionen im Weltraum. Sie versuchen durch horizontale Bewegungen, den Schüssen des Spielers auszuweichen. In unterschiedlichen Zeitabständen werden sie außerdem selbst versuchen, den Spieler anzugreifen. Dabei werden aber niemals mehr wie 2 Gegner gleichzeitig auf den Spieler schießen, trotzdem muss dieser versuchen so schnell wie Mögliche alle Gegner in seinem Blickfeld zu töten. Um dem Spieler die Chance zu geben rechtzeitig reagieren zu können, kündigen sich die Schüsse der Gegner stets auditiv an und sind somit vorhersehbar. Im verlauf des Spiels, werden die Gegner indirekt stärker, in dem sie sich beispielsweise schneller Bewegen oder in kürzeren Zeitabständen schießen.

Damit der Spieler konstant mit den Schüssen der Gegner konfrontiert wird, werden im Hintergrund verschiedene Wellen generiert. Diese Wellen wird der Spieler jedoch nicht direkt visuell wahrnehmen, da sie vom Spiel nicht angekündigt bzw. angezeigt werden. Zwischen den einzelnen Wellen, gibt es eine Phase in der für einen kurzen Moment keine Gegner mehr erzeugt werden und dem Spieler somit eine Möglichkeit der Pause geben. Damit im Spielverlauf die einzelnen Wellen immer stärker werden, variieren die Einstellungen der Gegner und die maximale Anzahl an Gegner pro Runde wird erhöht. Eine Welle ist außerdem nicht Zeitlich begrenzt, sondern wird beendet sobald alle Gegner getötet wurden. Erst dann wird die folgende Runde beginnen.

## Statistiken

Die Statistiken des aktuellen Spiels werden dem Spieler in Hologrammen während und am Ende des Spiels präsentiert. Verliert der Spieler, werden diese einmalig im Game Over Menü angezeigt und anschließend zurück gesetzt. Der Spieler beginnt somit mit jeder Runde von neuem.

# Level Design

## Der Weltraum

Der Weltraum besteht aus vier verschiednen 2D-Images die von Unity zu einer einzelnen Skybox zusammen gesetzt werden. Zusätzlich befindenen sich große und kleine Planeten mit unterschiedlichen Texturen im Weltraum, die als Fixpunkte für den Spieler dienen. Die Gegner erscheinen stets vor dem Spieler und befinden sich nur selten außerhalb des Blickwinkels, was dem Spiele die Chance gibt, schnell reagieren zu können.

## Das Cockpit

Der Spieler sitzt in einem Stuhl innerhalb eines kleinem Raumschiffes. Das Raumschiff hat hinter dem Stuhl eine Türe und LED Beleuchtung an den Wänden eingebaut. Vor dem Spieler befinden sich drei Holobildschirme: Der linke gibt die Anzahl eliminierter Gegner an, der rechte die verstrichene Zeit und der mittlere die Lebenspunkte und die Schildenergie. Neben den Holobildschirmen befindet sich ein roter Knopf, der deim Betätigen das Schutzschild aktiviert. Vor dem Spieler sind noch zwei Hebel angebracht, die einzig und allein als Designelement dienen und keine Interaktion bieten. Die Front des Schiffes ist einer Glaskuppel ähnlich, um den Spieler einen größtmöglichen Blick ins Weltall zu geben.

## Das Schutzschild

Das Schutzschild befindet um das gesamte Raumschiff herum und wird sowohl auditiv als auch visuell dem Spieler angezeigt.

# Menüs

## Hauptmenü

Im Hauptmenü hat der Spieler folgende Auswahlmöglichkeiten:

- Spiel starten
- Spiel beenden

## Pausemenü

Im Pausemenü hat der Spieler folgende Auswahlmöglichkeiten:

- Spiel fortsetzten
- Spiel beenden

## Game Over Menü

Im Game Over Menü sieht der Spieler die erspielte Statistik und hat folgende Möglichkeiten fortzufahren:

- Spiel erneut beginnen
- Spiel beenden

# Modelle & Texturen

Alle Modelle wurden als dreidimensionalen Objekte in Blender 2.8 modelliert und durch simple Texturen in Scene gesetzt. Die Größe der Modelle sind aufeinander abgestimmt, so dass im späteren Spiel zu einem stimmigen
Gesamtbild und sinnvollen Größenverhältnissen kommt. Folgende Modelle wurden für das Projekt erstellt:

- Zwei verschiedene Gegner.
- Verschiedene Planeten mit jeweils eigenen Texturen
- Das Cockpit
- Die Geschütztürme
- Das Steuerpult
- Die Joysticks
- Die Hologramme
- Der Knopf für das Schutzschild
- Der Stuhl
- Eine Alarmlampe

# Screenhots

<p align="center">
    <img src="Docs/Screenshots/Holograms On.PNG">
    <img src="Docs/Screenshots/Enemy Attack.PNG">
    <img src="Docs/Screenshots/Enemy Attack with Force Field Enabled.PNG">
    <img src="Docs/Screenshots/Game Over.PNG">
</p>

# Entwickler

- **Christof Schwarzenberger**: Modellierung/Level Designer
- **David Tomschitz**: Gameplay Programmierung
- **Duane Englert**: Game Design/Gameplay Programmierung
- **Sundar Arz**: KI Programmierung/Gameplay Programmierung

# License

MIT License

Copyright (c) 2021 Sundar Arz, Duane Englert, Christof Schwarzenberger, David Tomschitz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
