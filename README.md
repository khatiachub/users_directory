ფიზიკური პირის დამატება

o ფიზიკური პირის ძირითადი ინფორმაციის ცვლილება, რომელიც მოიცავს
შემდეგ მონაცემებს: სახელი, გვარი, სქესი, პირადი ნომერი, დაბადების
თარიღი, ქალაქი, ტელეფონის ნომრები

o ფიზიკური პირის სურათის ატვირთვა/ცვლილება (სურათი შევინახოთ
ფაილურ სისტემაში)

o ფიზიკური პირის დაკავშირებული ფიზიკური პირის დამატება/წაშლა

o ფიზიკური პირის წაშლა

o ფიზიკური პირის შესახებ სრული ინფორმაციის მიღება იდენტიფიკატორის
მეშვეობით (დაკავშირებული ფიზიკური პირების და სურათის ჩათვლით)

o ფიზიკური პირების სიის მიღება, სწრაფი ძებნის (სახელი, გვარი, პირადი
ნომრის მიხედვით (დამთხვევა sql like პრინციპით)), დეტალური ძებნის
(ყველა ველის მიხედვით) და paging-ის ფუნქციით

რეპორტი თუ რამდენი დაკავშირებული პირი ჰყავს თითოეულ ფიზიკურ
პირს, კავშირის ტიპის მიხედვით

 API-ის ყველა ოპერაციის შესრულების დროს უნდა მოხდეს შესაბამისი
მონაცემების სტრუქტურის და მთლიანობის ვალიდაცია. შეცდომის შემთხვევაში
შესაბამისი მესიჯის დაბრუნება. მესიჯები უნდა იყოს მარტივად ლოკალიზებადი.

 საერთო Action Filter რომელიც გადაამოწმებს მოთხოვნის მონაცემებს და
თუ არ არის ვალიდური დააბრუნებს შესაბამის შეცდომას

 API middleware- დაუმუშავებელი შეცდომების ლოგირებისთვის

 API middleware მოთხოვნის Accept-Language HTTP header პარამეტრის
შესაბამისი ლოკალიზაციის/ენის დაყენებისთვის

 გამოყენებულია Repository და Unit of work პატერნები

 დაცულია Clean Coding პრინციპები
