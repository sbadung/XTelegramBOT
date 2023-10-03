using XTelegramBOT.Permission;

namespace XTelegramBOT.Data.Collection;
public static class PermissionCollection
{
  public static Dictionary<PermissionTypeEnum, List<string>> COMMANDS_BY_PERMISSION = new() {
    { PermissionTypeEnum.ADMIN, CommandCollection.ADMIN_COMMANDS }
  };

  public static Dictionary<PermissionTypeEnum, List<long>> IDS_BY_PERMISSION = new() {
    { PermissionTypeEnum.ADMIN, IdCollection.ADMIN_IDS }
  };
}