# PatientAPi

## Structure
I've decided to use also a modular monalith as the basis of this project.
I chose this for two reasons. One I wanted to show how i would build this project 
in the same structure as ChipSoft is build and two because in my opinion this is the best way to create 
and API that is easy to maintain and easy to extend.

The project is build in a way that it is easy to add new modules. 
The `Application.Program` loops over all the modules by looking for services that implement the `IModule` interface.
Each module registers its own services, controllers and implements it own repositories. This way
each module is responsible for its own data and logic and its loosely coupled so it can be easily removed or replaced.
This ensures also that the modules are conform the DRY principle. 

I've created two modules; Patient and AuditLogs. As an example ill explain the Patient module. It's split up in multiple subprojects.
 - The patient project contains everything related to the API.
 - The contracts project contains dto's that are shared between modules, so its contained and you know when editing that i might affect other modules.
 - The core project contains the entities
 - The Infrastructure project contains the repositories and the database context with the entity configurations
 - The tests project contains the tests for the patient module

Each controller endpoint uses the mediatR to send a command. This is to ensure the controller doesnt contain any logic and its only repsonsible for handling the request and response.
The handler then handles the command and returns a response. This way the logic is contained in the handler and can be easily tested.
Finnally the response is mapped to a dto and returned to the controller.



## Notes
Due to time constraints i wasn't able to implement the following:
- Bus events and consuming
- Integration tests are written but not working
- Proper API response, with pagination and unified reponses
- The actual folder structure is not what it should be and doesnt properly reflect the sollution folder structure.
- The auditlog module only has the bare minimum to show how i would consume the bus event from the Patient domain event, but does not work or contain any tests.
- The API is untested as I use test driven development and the integration tests are not working.
- I wasn't able to research patient data encryption and what the universal standard is for handling this sensitive data.
- I've only added validation on the create endpoint, but not on the others.
- Gitignore is not properly set up, im sorry :)
