## Đánh giá
##### _Advantages_
- Dùng class Constants để lưu đường dẫn đến Resources khiến việc load resource dễ dàng và chỉ cần sửa đổi 1 nơi nếu có thay đổi
##### _Disadvantages_
- Sử dụng Resources.Load để lấy item nên nếu đổi tên item hoặc đổi vị trí thì phải đổi cả trong code
- Mỗi khi match thì item bị Destroy. Việc Instantiate và Destroy object liên tục đè nặng lên CPU
##### _Suggestion_
- Sử dụng New Input System để quản lý và thay đổi control dễ dàng hơn, không phải sửa trong code
- Sắp xếp texture theo folder từng hạng mục
- Những folder nào hay sử dụng, gộp lại trong một folder tên là ký hiệu như "!, _" để folder đó luôn hiện trên đầu
