# CareerNet Microservices Architecture

CareerNet, işverenlerin şirketlerini ve iş ilanlarını yönetebileceği, ölçeklenebilir bir mikroservis mimarisi üzerine inşa edilmiştir. Bu proje, .NET Core ve Elasticsearch gibi teknolojilerle, bağımsız servisler ve bir API Gateway kullanılarak tasarlanmıştır.

## Projeler ve Yapı

### 1-) CareerNetApiGateway

CareerNet Gateway, tüm mikroservisler arasında bir yönlendirme katmanı görevi görür. İşveren ve ilan servislerine gelen istekleri karşılar.Ocelot kullanılarak API yönlendirilmesi yapılmıştır

### 2-) CareerNetCompany

CareerNetCompany servisi, işverenlerin kayıt, güncelleme, silme ve sorgulama işlemlerini yürütür. İşveren bilgileri PostgreSQL veritabanında saklanır ve veriye erişim, Soğan Mimarisi (Onion Architecture) ile yönetilir.

#### Kullanılan Teknolojiler ve Desenler:
- .**Net 8.0**
- **Entity Framework Core**: PostgreSQL ile veri yönetimi için Code-First yaklaşımı.
- **Onion Architecture**: İş mantığı ve veri erişimi arasında katmanlı bir yapı sağlamak için kullanılan mimari desen.
- **Dependency Injection (IoC)**:Microsof DI bağımlılık yönetimi
- **PostgreSQL**:İşveren verileri için kullanılan ilişkisel veritabanı.
- **RabbitMQ**:CareerNetJob servisi ile işveren ve ilan arasında iletişim sağlamak için mesajlaşma altyapısı.


### 3-) CareerNetJob

CareerNetJob servisi, iş ilanlarının oluşturulması ve sorgulanması için yapılandırılmıştır. İlanlar, güçlü arama ve filtreleme için Elasticsearch üzerine kaydedilir ve yönetilir.

#### Kullanılan Teknolojiler ve Desenler:
- .**Net 8.0**
- **Elasticsearch**: İş ilanlarının hızlı sorgulanması ve tam metin aramaları için kullanılan arama motoru.
- **N-Tier Architecturee**: Katmanlı yapı ile veri erişimi, iş mantığı ve API erişimini ayrıştıran mimari desen.
- **Dependency Injection (IoC)**:Microsof DI bağımlılık yönetimi
- **Redis**:Sakıncalı kelime listesi gibi nadiren değişen veriler için kullanılan hızlı önbellek çözümü.
- **RabbitMQ**:İş ilanları oluşturulmadan önce işverenin ilan yayınlama hakkını kontrol eden mesajlaşma sistemi.
- **MassTransit**:RabbitMQ ile mikroservisler arasında mesajlaşmayı sağlayan kütüphane.

##### Documentations
- [CareerNet Genel Proje Dökümanı](https://github.com/ahmtklm/CareerNet/blob/master/docs/CareerNet.docx)
- [CareerNet Job Mikroservisi Dökümanı](https://github.com/ahmtklm/CareerNet/blob/master/docs/CareerNetJob.docx)
- [CareerNet Company Mikroservisi Dökümanı](https://github.com/ahmtklm/CareerNet/blob/master/docs/CareerNetCompany.docx)
