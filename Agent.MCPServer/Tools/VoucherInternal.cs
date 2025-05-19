using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Agent.MCPServer.Tools;


public class VoucherInternal
{

  private readonly List<Voucher> vouchers = [];
  public VoucherInternal()
  {
    vouchers = SeedVoucher();
  }


  [McpServerTool, Description("Get voucher by employee code")]
  public Voucher GetVoucher(string employeeCode)
  {
    if (string.IsNullOrEmpty(employeeCode ))
      return null;
    var voucher = vouchers.FirstOrDefault(v => v.EmployeeCode == employeeCode && v.VoucherItems.Any(x => x.Status == StatusType.Active));
    if (voucher == null)
      return null;
    return voucher;
  }

  public static List<Voucher> SeedVoucher()
  {
    return
    [
        new Voucher
        {
            EmployeeCode = "N001",
            EmployeeName = "Nguyễn Văn A",
            VoucherItems =
            [
                new() {
                    ItemName = "Item1",
                    Percent = 10,
                    ExpirationDate = DateTime.Now.AddMonths(1),
                    Status = StatusType.Active
                },
                new() {
                    ItemName = "Item2",
                    Percent = 20,
                    ExpirationDate = DateTime.Now.AddMonths(2),
                    Status = StatusType.InActive
                }
            ]
        },
        new Voucher
        {
            EmployeeCode = "N002",
            EmployeeName = "Nguyễn Văn B",
            VoucherItems =
            [
                new() {
                    ItemName = "Item3",
                    Percent = 10,
                    ExpirationDate = DateTime.Now.AddMonths(1),
                    Status = StatusType.Active
                },
                new() {
                    ItemName = "Item4",
                    Percent = 50,
                    ExpirationDate = DateTime.Now.AddMonths(2),
                    Status = StatusType.InActive
                }
            ]
        }
    ];
  }
};



public class Voucher
{
  [Description("Mã nhân viên")]
  public string EmployeeCode { get; set; } = default!;

  [Description("Tên nhân viên")]
  public string EmployeeName { get; set; } = default!;

  [Description("Danh sách voucher")]
  public List<VoucherItem> VoucherItems { get; set; } = [];

	[Description("Danh sách voucher")]
	public class VoucherItem
  {
    [Description("Tên voucher")]
    public string ItemName { get; set; } = default!;

    [Description("Phần trăm")]
    public int Percent { get; set; }

    [Description("Ngày hết hạn")]
    public DateTime ExpirationDate { get; set; }

    [Description("Trạng thái")]
    public StatusType Status { get; set; } = StatusType.Active;

  }
}

public enum StatusType
{
  [Description("Đang hoạt động")]
  Active,

  [Description("Ngừng hoạt động")]
  InActive
}