﻿//  // Copyright (c) Microsoft.  All rights reserved.// //  Licensed under the Apache License, Version 2.0 (the "License");//  you may not use this file except in compliance with the License.//  You may obtain a copy of the License at//    http://www.apache.org/licenses/LICENSE-2.0// //  Unless required by applicable law or agreed to in writing, software//  distributed under the License is distributed on an "AS IS" BASIS,//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.//  See the License for the specific language governing permissions and//  limitations under the License.namespace Microsoft.Azure.Commands.ApiManagement.ServiceManagement.Commands{    using System;    using System.IO;    using System.Management.Automation;    using System.Text;    using Microsoft.Azure.Commands.ApiManagement.ServiceManagement.Models;    [Cmdlet(VerbsCommon.Set, "AzureApiManagementPolicy", DefaultParameterSetName = TenantLevel)]    [OutputType(typeof(bool))]    public class SetAzureApiManagementPolicy : AzureApiManagementCmdletBase    {        private const string DefaultFormat = "application/vnd.ms-azure-apim.policy+xml";        private const string TenantLevel = "Tenant level";        private const string ProductLevel = "Product level";        private const string ApiLevel = "API level";        private const string OperationLevel = "Operation level";        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = true,             HelpMessage = "Instance of PsApiManagementContext. This parameter is required.")]        [ValidateNotNullOrEmpty]        public PsApiManagementContext Context { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = false,             HelpMessage = "Format of the policy. This parameter is optional. Default value is 'application/vnd.ms-azure-apim.policy+xml'.")]        public String Format { get; set; }        [Parameter(            ParameterSetName = ProductLevel,            ValueFromPipelineByPropertyName = true,             Mandatory = true,             HelpMessage = "Identifier of existing product. If specified will set product-scope policy. This parameters is required.")]        public String ProductId { get; set; }        [Parameter(            ParameterSetName = ApiLevel,            ValueFromPipelineByPropertyName = true,             Mandatory = true,             HelpMessage = "Identifier of existing API. If specified will set API-scope policy. This parameters is required.")]        [Parameter(            ParameterSetName = OperationLevel,            ValueFromPipelineByPropertyName = true,            Mandatory = true,            HelpMessage = "Identifier of existing API. If specified will set API-scope policy. This parameters is required.")]        public String ApiId { get; set; }        [Parameter(            ParameterSetName = OperationLevel,            ValueFromPipelineByPropertyName = true,            Mandatory = true,             HelpMessage = "Identifier of existing operation. If specified with ApiId will set operation-scope policy. This parameters is required.")]        public String OperationId { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = false,             HelpMessage = "Policy document as a string. This parameter is required if -PolicyFilePath not specified.")]        public String Policy { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = false,             HelpMessage = "Policy document file path. This parameter is required if -Policy not specified.")]        public String PolicyFilePath { get; set; }        [Parameter(            ValueFromPipelineByPropertyName = true,             Mandatory = false,             HelpMessage = "If specified will write true in case operation succeeds. This parameter is optional. Default value is false.")]        public SwitchParameter PassThru { get; set; }        public override void ExecuteApiManagementCmdlet()        {            Stream stream = null;            try            {                if (!string.IsNullOrWhiteSpace(Policy))                {                    stream = new MemoryStream(Encoding.UTF8.GetBytes(Policy));                }                else if (!string.IsNullOrEmpty(PolicyFilePath))                {                    stream = File.OpenRead(PolicyFilePath);                }                else                {                    throw new PSInvalidOperationException("Either Policy or PolicyFilePath should be specified.");                }                string format = Format ?? DefaultFormat;                switch (ParameterSetName)                {                    case TenantLevel:                        Client.PolicySetTenantLevel(Context, format, stream);                        break;                    case ProductLevel:                        Client.PolicySetProductLevel(Context, format, stream, ProductId);                        break;                    case ApiLevel:                        Client.PolicySetApiLevel(Context, format, stream, ApiId);                        break;                    case OperationLevel:                        if (string.IsNullOrWhiteSpace(ApiId))                        {                            throw new PSArgumentNullException("ApiId");                        }                        Client.PolicySetOperationLevel(Context, format, stream, ApiId, OperationId);                        break;                    default:                        throw new InvalidOperationException(string.Format("Parameter set name '{0}' is not supported.", ParameterSetName));                }                if (PassThru)                {                    WriteObject(true);                }            }            finally            {                if (stream != null)                {                    stream.Dispose();                }            }        }    }}         H e l p M e s s a g e   =   " I d e n t i f i e r   o f   e x i s t i n g   o p e r a t i o n .   I f   s p e c i f i e d   w i t h   A p i I d   w i l l   s e t   o p e r a t i o n - s c o p e   p o l i c y .   T h i s   p a r a m e t e r s   i s   r e q u i r e d . " ) ]                  p u b l i c   S t r i n g   O p e r a t i o n I d   {   g e t ;   s e t ;   }                   [ P a r a m e t e r (                          V a l u e F r o m P i p e l i n e B y P r o p e r t y N a m e   =   t r u e ,                            M a n d a t o r y   =   f a l s e ,                            H e l p M e s s a g e   =   " P o l i c y   d o c u m e n t   a s   a   s t r i n g .   T h i s   p a r a m e t e r   i s   r e q u i r e d   i f   - P o l i c y F i l e P a t h   n o t   s p e c i f i e d . " ) ]                  p u b l i c   S t r i n g   P o l i c y   {   g e t ;   s e t ;   }                   [ P a r a m e t e r (                          V a l u e F r o m P i p e l i n e B y P r o p e r t y N a m e   =   t r u e ,                            M a n d a t o r y   =   f a l s e ,                            H e l p M e s s a g e   =   " P o l i c y   d o c u m e n t   f i l e   p a t h .   T h i s   p a r a m e t e r   i s   r e q u i r e d   i f   - P o l i c y   n o t   s p e c i f i e d . " ) ]                  p u b l i c   S t r i n g   P o l i c y F i l e P a t h   {   g e t ;   s e t ;   }                   [ P a r a m e t e r (                          V a l u e F r o m P i p e l i n e B y P r o p e r t y N a m e   =   t r u e ,                            M a n d a t o r y   =   f a l s e ,                            H e l p M e s s a g e   =   " I f   s p e c i f i e d   w i l l   w r i t e   t r u e   i n   c a s e   o p e r a t i o n   s u c c e e d s .   T h i s   p a r a m e t e r   i s   o p t i o n a l .   D e f a u l t   v a l u e   i s   f a l s e . " ) ]                  p u b l i c   S w i t c h P a r a m e t e r   P a s s T h r u   {   g e t ;   s e t ;   }                   p u b l i c   o v e r r i d e   v o i d   E x e c u t e A p i M a n a g e m e n t C m d l e t ( )                  {                          S t r e a m   s t r e a m   =   n u l l ;                          t r y                          {                                  i f   ( ! s t r i n g . I s N u l l O r W h i t e S p a c e ( P o l i c y ) )                                  {                                          s t r e a m   =   n e w   M e m o r y S t r e a m ( E n c o d i n g . U T F 8 . G e t B y t e s ( P o l i c y ) ) ;                                  }                                  e l s e   i f   ( ! s t r i n g . I s N u l l O r E m p t y ( P o l i c y F i l e P a t h ) )                                  {                                          s t r e a m   =   F i l e . O p e n R e a d ( P o l i c y F i l e P a t h ) ;                                  }                                  e l s e                                  {                                          t h r o w   n e w   P S I n v a l i d O p e r a t i o n E x c e p t i o n ( " E i t h e r   P o l i c y   o r   P o l i c y F i l e P a t h   s h o u l d   b e   s p e c i f i e d . " ) ;                                  }                                   s t r i n g   f o r m a t   =   F o r m a t   ? ?   D e f a u l t F o r m a t ;                                  s w i t c h   ( P a r a m e t e r S e t N a m e )                                  {                                          c a s e   T e n a n t L e v e l :                                                  C l i e n t . P o l i c y S e t T e n a n t L e v e l ( C o n t e x t ,   f o r m a t ,   s t r e a m ) ;                                                  b r e a k ;                                          c a s e   P r o d u c t L e v e l :                                                  C l i e n t . P o l i c y S e t P r o d u c t L e v e l ( C o n t e x t ,   f o r m a t ,   s t r e a m ,   P r o d u c t I d ) ;                                                  b r e a k ;                                          c a s e   A p i L e v e l :                                                  C l i e n t . P o l i c y S e t A p i L e v e l ( C o n t e x t ,   f o r m a t ,   s t r e a m ,   A p i I d ) ;                                                  b r e a k ;                                          c a s e   O p e r a t i o n L e v e l :                                                  i f   ( s t r i n g . I s N u l l O r W h i t e S p a c e ( A p i I d ) )                                                  {                                                          t h r o w   n e w   P S A r g u m e n t N u l l E x c e p t i o n ( " A p i I d " ) ;                                                  }                                                  C l i e n t . P o l i c y S e t O p e r a t i o n L e v e l ( C o n t e x t ,   f o r m a t ,   s t r e a m ,   A p i I d ,   O p e r a t i o n I d ) ;                                                  b r e a k ;                                          d e f a u l t :                                                  t h r o w   n e w   I n v a l i d O p e r a t i o n E x c e p t i o n ( s t r i n g . F o r m a t ( " P a r a m e t e r   s e t   n a m e   ' { 0 } '   i s   n o t   s u p p o r t e d . " ,   P a r a m e t e r S e t N a m e ) ) ;                                  }                                   i f   ( P a s s T h r u )                                  {                                          W r i t e O b j e c t ( t r u e ) ;                                  }                          }                          f i n a l l y                          {                                  i f   ( s t r e a m   ! =   n u l l )                                  {                                          s t r e a m . D i s p o s e ( ) ;                                  }                          }                  }          }  } 