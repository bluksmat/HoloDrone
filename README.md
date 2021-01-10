# HoloDrone

## Test Project Made for: <img src="http://immersiveform.com/images/IF_logo.svg" height="128" style="width: 10%; overflow:hiden; margin: -52px; margin-left:-25px"/>


### Features:

### Project specification:
- Application target: Hololens 2
Project meet DI (Dependency Injection) requirements using Extenject for future extension of the application.

### Core elements:
- [ ] Floating menu with buttons allowing to interact with model. Menu should include at least 3 buttons: Adjust, Explode, Info.
  - [ ] Adjust - Will display BoundingBox/ BoundsControl allowing to manipulate object: Moveing, Rotationg and etc.
  - [ ] Explode - Display disassembled model (explode)
  - [ ] Info - Display Tooltipy with names of parts.
- [ ] AppManager - Object wich manage application with optional parameters 

### Issues:
- Drone model scale was incorrect - had to be refactored to 0.1 of the original size.

### Developer improvement suggestions:
- Eye tracking trigger activation of tooltips for more detail description
