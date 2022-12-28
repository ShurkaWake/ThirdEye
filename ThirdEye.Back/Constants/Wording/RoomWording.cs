namespace ThirdEye.Back.Constants.Wording
{
    public static class RoomWording
    {
        public const string NameRequiredMessage = "Name is required field";
        public const string BusinessIdRequiredMessage = "BusinessId is required field";
        public const string IdRequiredMessage = "Id is required field";

        public const string BusinessNotFoundMessage = "There is no business matches with providen data";
        public const string UnableToCreateMessage = "Unable to create this room";
        public const string UnexpectedErrorMessage = "Something went wrong :( Please try again later";
        public const string RoomNotFoundMessage = "There is no room matches with providen data";
        public const string UnableToDeleteMessage = "Unable to delete this room";
        public const string UnableToUpdateMessage = "Unable to update this room";
        public const string UserIsNotWorkerMessage = "User is not worker of this business. No access granted";
        public const string UserIsNotManagerMessage = "This action can be done only by business manager. No access granted";
    }
}
