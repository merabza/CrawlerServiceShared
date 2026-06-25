namespace CrawlerServiceShared.Contracts;

//GetByName-ტიპის ენდპოინტების პასუხის გადასაცემი არა-null გარსი:
//Value=null ნიშნავს, რომ ჩანაწერი ვერ მოიძებნა (ეს არ არის შეცდომა).
public sealed class ApiNullableResult<T>
{
    public T? Value { get; set; }
}
