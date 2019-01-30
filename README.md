# JWA-Randomizer
Randomizes your dinosaur team for Jurassic World Alive.  Can either completely randomize, weighted randomize, or balance randomize.

Still a work in progress.

Input your available dinosaurs into an XML file formatted like below for the application to load your available dinosaurs.

```html
<dinosaurs>
  <dinosaur>
    <name>Dracorex Gen 2</name>
    <level>4</level>
  </dinosaur>
  <dinosaur>
    <name>Einiasuchus</name>
    <level>7</level>
  </dinosaur>
  <dinosaur>
    <name>Tarbosaurus</name>
    <level>5</level>
  </dinosaur>
</dinosaurs>
```

Then, select one of the following randomization options for generating your team:

1. Standard = Complete randomization
2.  Weighted = Favors higher level dinosaurs
3.  Balanced = Attempts to create a team of dinosaurs of various, complementary types (i.e. Chomper, Raptor, Tank, etc.)
