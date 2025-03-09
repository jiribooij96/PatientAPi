namespace PatientApi.Modules.Patient.Patient.Exceptions;

public class EntityAlreadyExistsException(string message) : Exception(message);