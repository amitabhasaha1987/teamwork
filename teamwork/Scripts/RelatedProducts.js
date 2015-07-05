function RelatedProducts()
{

}

var RelatedProducts_$form = '';
var spinningimage = '';

RelatedProducts.prototype.LoadProducts = function(e)
{
    var $selectproduct = RelatedProducts_$form.find("select[id='product']");
    var $selectrelatedproduct = RelatedProducts_$form.find("select[id='relatedproduct']");
    var categoryvalue = RelatedProducts_$form.find("select[id='category']").val();

    if(categoryvalue != '')
    {
        $.ajax({
            url: '/Products/GetProductsByCategory',
            data: JSON.stringify({ category: categoryvalue }),
            type: "POST",
            dataType: 'json',
            contentType: 'application/json'
        }).success(function (data) {
            
        })
        .done(function (data) {
            
            $selectproduct.empty().append("<option value=''>Select Product</option>");
            $selectrelatedproduct.empty().append("<option value=''>Select Product</option>");
            $.each(data, function (i, val) {
                $selectproduct.append("<option value='" + val.id + "'>" + val.name + "</option>");
                $selectrelatedproduct.append("<option value='" + val.id + "'>" + val.name + "</option>");
            })
        })//end done
        .error(function (xhr, ajaxOptions, thrownError) {
            
        });
    }
        
    else
    {
        $selectproduct.empty().append("<option value=''>Select Product</option>");
        $selectrelatedproduct.empty().append("<option value=''>Select Product</option>");
        //RelatedProducts.prototype.$form.find("select[id='category']").next('span').val(' Select category')
    }
}


RelatedProducts.prototype.AddRelatedProducts = function()
{
    RelatedProducts_$form.find("a[id='addRelatedProduct']").next('span').html(spinningimage);
    var $selectproduct = RelatedProducts_$form.find("select[id='product']");
    var $selectrelatedproduct = RelatedProducts_$form.find("select[id='relatedproduct']");

    $selectproduct.next('span').text('');
    $selectrelatedproduct.next('a').next('span').text('');
    
    if($selectproduct.val() != '' && $selectrelatedproduct.val() != '')
    {
        $.ajax({
            url: '/Products/AddRelatedProduct',
            data: JSON.stringify({
                productid: $selectproduct.val(),
                relatedprodid: $selectrelatedproduct.val(),
                relatedProdName: $selectrelatedproduct.find("option:selected").text()
            }),
            type: "POST",
            dataType: 'json',
            contentType: 'application/json'
        })
        .success(function (result) {
            debugger;
            if (result.success == 1)
            {
                RelatedProducts_$form.find("div[id='relatedProductList']").append(result.Html);
            }
            else
            {
                alert('Some error occurs, try again later.');
            }

        })
        .error(function (error) {
            alert('Some error occurs, try again later.');
        })
    }
    else
    {
        if ($selectproduct.val() != '') { $selectproduct.next('span').text('Select product'); }
        if ($selectrelatedproduct.val() != '') { $selectrelatedproduct.next('a').next('span').text('Select related product'); }
    }
    RelatedProducts_$form.find("a[id='addRelatedProduct']").next('span').html('');
}

RelatedProducts.prototype.LoadRelatedProducts = function(e)
{
    
    var $selectproduct = RelatedProducts_$form.find("select[id='product']");
    if ($selectproduct.val() != '')
    {
        $.ajax({
            url: '/Products/LoadRelatedProduct',
            data: JSON.stringify({
                prodId: $selectproduct.val()}),
            type: "POST",
            dataType: 'json',
            contentType: 'application/json'
        })
        .success(function (result) {
            RelatedProducts_$form.find("div[id='relatedProductList']").empty();
            RelatedProducts_$form.find("div[id='relatedProductList']").append(result.Html);
        })
        .error(function (error) {
            alert('Some error occurs, try again later.');
        })
    }
}

$(document).on('click', '.removeRelatedProduct', function () {

    debugger;
    var id = $(this).attr('data-productid');
    var obj = $(this);

    $.ajax({
        url: '/Products/removeRelatedProduct',
        data : JSON.stringify({ relatedProdMapid : id }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json'
    })
    .success(function (result) {
        if(result == 1)
        {
            obj.parent('div').parent('div').remove();
        }
        else
            alert('Error occures, please try again later');
    })
    .error(function (error) {
        alert('Error occures, please try again later');
    })
});



$(window).load(function () {

    spinningimage = "<i class='ace-icon fa fa-spinner fa-spin orange bigger-125'></i>";
    RelatedProducts_$form = $("form[id='formRelatedProduct']");

    RelatedProducts_$form.find("select[id='category']").on("change", function (e) {
        RelatedProducts.prototype.LoadProducts(e);
    });

    RelatedProducts_$form.find("a[id='addRelatedProduct']").on('click', function () {
        RelatedProducts.prototype.AddRelatedProducts();
    });

    RelatedProducts_$form.find("select[id='product']").on("change", function (e) {
        RelatedProducts.prototype.LoadRelatedProducts(e);
    });
    
})