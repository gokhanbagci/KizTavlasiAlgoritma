# KizTavlasiAlgoritma
Kız Tavlası, Türkiye'de oynanan bir tür masa oyunudur. Genellikle standart bir tavla tahtası üzerinde oynanır ancak kuralları klasik tavladan farklıdır. C# ile bu oyunun algoritma örneğini paylaşıyorum.

## Oyun Kuralları

1. **Başlangıç Dizilimi:** Her oyuncu, başlangıçta kendi tarafında 15 pul dizilmiştir. Dizilim sırası soldan sağa doğru 3-3-3-2-2-2 şeklindedir.
2. **Zar Atma:** Oyuncular sırayla iki zar atar. Zar sonuçlarına göre, ilgili sıradaki pullar hareket ettirilir.
    - Eğer zarlar çift gelirse (örneğin 6-6), ilgili sıradaki tüm pullar hareket ettirilir.
    - Eğer zarlar farklı gelirse (örneğin 6-5), ilgili sıradaki pullardan birer pul hareket ettirilir.
3. **Pul İndirme:** Pullar, zar sonucuna göre sırayla aşağı indirilir. Eğer indirilecek pul yoksa sıra karşı oyuncuya geçer.
4. **Toplama Aşaması:** Tüm pullar indirildikten sonra toplama aşamasına geçilir. Aynı zar atma kuralları burada da geçerlidir. İlgili sıradaki pullar tahtadan çıkarılır.
