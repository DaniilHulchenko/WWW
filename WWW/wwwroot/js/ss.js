<script>
    document.getElementById("searchForm").addEventListener("submit", function (event) {
        event.preventDefault(); // Предотвращаем отправку формы

    var searchTerm = document.querySelector('input[name="searchTerm"]').value;
    window.location.href = '/Article?searchTerm=' + encodeURIComponent(searchTerm);
                        });
</script>