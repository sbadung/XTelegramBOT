using XTelegramBOT.Data.Collection;

namespace XTelegramBOT.Permission;

public static class PermissionController
{
  public static PermissionTypeEnum RequiredPermissionToRunCommand(string command)
  {
    foreach (var commandByPermission in PermissionCollection.COMMANDS_BY_PERMISSION)
    {
      bool hasPermission = commandByPermission.Value.Contains(command);
      if (hasPermission) return commandByPermission.Key;
    }

    return PermissionTypeEnum.USER;
  }

  public static bool CanRunCommand(long id, string command)
  {
    PermissionTypeEnum permissionRequired = RequiredPermissionToRunCommand(command);
    return permissionRequired == PermissionTypeEnum.USER || PermissionCollection.IDS_BY_PERMISSION[permissionRequired].Contains(id);
  }
}