namespace SoundSoulCore.Response.Common
{
    public class ResponseApi
    {
        #region Mensajes

        private const string MessageGet = "Información del registro obtenida exitosamente.";
        private const string MessageGetReport = "Información del reporte obtenida exitosamente.";
        private const string MessageGetAll = "Información obtenida exitosamente.";
        private const string MessageUpdate = "Información actualizada exitosamente.";
        private const string MessageCreate = "Información creada exitosamente.";
        private const string MessageChangeActive = "El estado del registo se cambio exitosamente.";
        private const string MessageDelete = "El registro se eliminó completamente de manera correcta.";
        private const string MessageInspector = "El inspector guardo el Historico de la transacción correctamente.";
        private const string MessageLogin = "Inicio de sesión exitosa";
        private const string MessageLogout = "Cierre de sesión";
        private const string MessageOther = "Operación realizada exitosamente.";

        #endregion


        public bool Result { get; set; }

        public TypeOperation TypeOperation { get; set; }

        public string? Message { get; set; }

        public string? DescriptionError { get; set; }

        public object? Data { get; set; }

        public void PackData(object? Data, TypeOperation typeOperation)
        {
            try
            {
                this.TypeOperation = typeOperation;
                this.Data = Data;
                switch (typeOperation)
                {
                    case TypeOperation.Get:
                        OperationSuccess(MessageGet);
                        break;
                    case TypeOperation.GetReport:
                        OperationSuccess(MessageGetReport);
                        break;
                    case TypeOperation.GetAll:
                        OperationSuccess(MessageGetAll);
                        break;
                    case TypeOperation.Update:
                        OperationSuccess(MessageUpdate);
                        break;
                    case TypeOperation.Create:
                        OperationSuccess(MessageCreate);
                        break;
                    case TypeOperation.ChangeState:
                        OperationSuccess(MessageChangeActive);
                        break;
                    case TypeOperation.Delete:
                        OperationSuccess(MessageDelete);
                        break;
                    case TypeOperation.Inspector:
                        OperationSuccess(MessageInspector);
                        break;
                    case TypeOperation.Login:
                        OperationSuccess(MessageLogin);
                        break;
                    case TypeOperation.Logout:
                        OperationSuccess(MessageLogout);
                        break;
                    case TypeOperation.Other:
                        OperationSuccess(MessageOther);
                        break;
                }
            }
            catch (Exception ex)
            {
                OperationFail(messageError: ex.Message, typeOperation);
            }
        }

        public void PackData(object data, TypeOperation typeOperation, string messageSuccessCustom)
        {
            try
            {
                this.TypeOperation = typeOperation;
                this.Data = Data;
                switch (typeOperation)
                {
                    case TypeOperation.Get:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.GetAll:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.Update:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.Create:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.ChangeState:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.Delete:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.Login:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.Logout:
                        OperationSuccess(messageSuccessCustom);
                        break;
                    case TypeOperation.Other:
                        OperationSuccess(messageSuccessCustom);
                        break;
                }
            }
            catch (Exception ex)
            {
                OperationFail(messageError: ex.Message, typeOperation);
            }
        }

        public void PackData(TypeOperation typeOperation)
        {
            try
            {
                this.TypeOperation = typeOperation;
                switch (typeOperation)
                {
                    case TypeOperation.Get:
                        OperationSuccess(MessageGet);
                        break;
                    case TypeOperation.GetAll:
                        OperationSuccess(MessageGetAll);
                        break;
                    case TypeOperation.Update:
                        OperationSuccess(MessageUpdate);
                        break;
                    case TypeOperation.Create:
                        OperationSuccess(MessageCreate);
                        break;
                    case TypeOperation.ChangeState:
                        OperationSuccess(MessageChangeActive);
                        break;
                    case TypeOperation.Delete:
                        OperationSuccess(MessageDelete);
                        break;
                    case TypeOperation.Inspector:
                        OperationSuccess(MessageInspector);
                        break;
                    case TypeOperation.Login:
                        OperationSuccess(MessageLogin);
                        break;
                    case TypeOperation.Logout:
                        OperationSuccess(MessageLogout);
                        break;
                    case TypeOperation.Other:
                        OperationSuccess(MessageOther);
                        break;
                }
            }
            catch (Exception ex)
            {
                OperationFail(messageError: ex.Message, typeOperation);
            }
        }

        private void OperationSuccess(string messageSuccess)
        {
            Result = true;
            Message = messageSuccess;
            DescriptionError = string.Empty;
        }

        public void OperationFail(string messageError, TypeOperation typeOperation)
        {
            Result = false;
            Message = "Operacion no realizada";
            TypeOperation = typeOperation;
            DescriptionError = messageError;
            Data = null;
        }

        public BadResponse OperationFailResponse(string messageError, TypeOperation typeOperation)
        {
            return new BadResponse
            {
                Result = false,
                Message = "Operacion no realizada",
                TypeOperation = typeOperation,
                DescriptionError = messageError,
                Data = null
            };
        }
    }
}
